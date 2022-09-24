using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using _99xAssessment2.Repository;
using _99xAssessment2.Repository.ORM;
using _99xAssessment2.Tests.TestModels;
using _99xAssessment2.Utils;
using NUnit.Framework;

namespace _99xAssessment2.Tests.RepoTest
{
    [TestFixture]
    public class DalTestFixture
    {
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string FileName = "ExcelSampleAccount.xlsx";
        public static long TestUserId = 0;
        public const string UnitTestUserName = "unittest@demo.com";
        public static List<string> UnitTestAccountNames = new List<string>()
            { "TestAccount1", "TestAccount2", "TestAccount3", "TestAccount4", "TestAccount5" };
        public static decimal ExcelSampleSum = 7422;

        [SetUp]
        public void AddTestUnitTestUser()
        {
            var dbUser = DAL.GetUsers().FirstOrDefault(x => x.Username == UnitTestUserName);
            if (dbUser == null)
            {
                DAL.AddOrUpdateUser(new User
                {
                    Username = UnitTestUserName,
                    Password = "abc"
                });
            }

            SetUnitTestUserId();
        }

        private void SetUnitTestUserId()
        {
            var dbUser = DAL.GetUsers().FirstOrDefault(x => x.Username == UnitTestUserName);
            if (dbUser != null)
                TestUserId = dbUser.Id;
        }

        [TearDown]
        public void ResetTestData()
        {
            var accountsToBeDeleted = DAL.GetAccounts().Where(x => x.AccountName.Contains("Test")).ToList();
            if (accountsToBeDeleted.Any())
            {
                DAL.DeleteAccounts(accountsToBeDeleted);
            }
        }

        [Test]
        public void TestCreateAccountFromExcel()
        {
            var objFile = UploadAndProcessExcelSampleFile();

            DAL.ProcessAndUpdateAccountInfo(objFile, DateTime.Now.Year, DateTime.Now.Month, TestUserId);

            var afterUploadAccounts = DAL.GetAccounts().Where(x => UnitTestAccountNames.Contains(x.AccountName));

            Assert.AreEqual(true, afterUploadAccounts.Any());

        }

        [Test]
        public void TestCreateAccountAndAccountJournalFromExcel()
        {
            var objFile = UploadAndProcessExcelSampleFile();

            DAL.ProcessAndUpdateAccountInfo(objFile, DateTime.Now.Year, DateTime.Now.Month, TestUserId);

            var afterUploadAccountJournals = DAL.GetAccounts().Where(x => UnitTestAccountNames.Contains(x.AccountName) && x.AccountJournals.Any());

            Assert.AreEqual(true, afterUploadAccountJournals.Any());
        }

        private static HttpPostedFileBase UploadAndProcessExcelSampleFile()
        {
            byte[] bytes = File.ReadAllBytes("../../ExcelSample/ExcelSampleAccount.xlsx");

            HttpPostedFileBase objFile = (HttpPostedFileBase)new
                HttpPostedFileBaseCustom(new MemoryStream(bytes), ContentType, FileName);

            var beforeUploadAccounts = DAL.GetAccounts().Where(x => UnitTestAccountNames.Contains(x.AccountName));

            Assert.AreEqual(false, beforeUploadAccounts.Any());
            return objFile;
        }

        [Test]
        public void TestInValidExcelImports()
        {
            byte[] bytes = File.ReadAllBytes("../../ExcelSample/InValidFile.pdf");

            HttpPostedFileBase objFile = (HttpPostedFileBase)new
                HttpPostedFileBaseCustom(new MemoryStream(bytes), "application/pdf", FileName);

            var beforeUploadAccounts = DAL.GetAccounts().Where(x => UnitTestAccountNames.Contains(x.AccountName));

            Assert.AreEqual(false, beforeUploadAccounts.Any());

            try
            {
                DAL.ProcessAndUpdateAccountInfo(objFile, DateTime.Now.Year, DateTime.Now.Month, TestUserId);
                Assert.Fail("Pdf should be rejected.");
            }
            catch (Exception e)
            {
                Assert.Pass("PDF is rejected with stacktrace:" + e.StackTrace);
            }
        }

        [Test]
        public void TestBalanceReport()
        {
            var objFile = UploadAndProcessExcelSampleFile();

            DAL.ProcessAndUpdateAccountInfo(objFile, DateTime.Now.Year, DateTime.Now.Month, TestUserId);

            var afterUploadAccountJournals = DAL.GetAccounts().Where(x => UnitTestAccountNames.Contains(x.AccountName) && x.AccountJournals.Any()).ToList();

            Assert.AreEqual(true, afterUploadAccountJournals.Any());

            string balanceAsOf = afterUploadAccountJournals.SelectMany(x => x.AccountJournals).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).FirstOrDefault().ToAsOfBalance();

            //As of balance caption must be the last date/month
            Assert.AreEqual(true, balanceAsOf.Contains(DateTime.Now.Year.ToString()));

            var accJournals = afterUploadAccountJournals.SelectMany(x => x.AccountJournals).CalculateFinalBalance();

            Assert.AreEqual(ExcelSampleSum, accJournals.Select(x => x.SummedValues).Sum());
        }
    }
}
