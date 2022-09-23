using System;
using System.Linq;
using System.Web.Mvc;
using _99xAssessment2.Controllers;
using _99xAssessment2.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _99xAssessment2.Tests.Controllers
{
    [TestClass]
    public class UserAndAccountTestFixture
    {
        [TestMethod]
        public void TestAddAdminUser()
        {
            AccountController controller = new AccountController();

            var randomUserName = $"admin_{new Random().Next(111111, 999999)}@demo.com";

            ViewResult result = controller.AddSuperAdminUser(randomUserName) as ViewResult;

            Assert.AreEqual(true, DAL.GetUsers().Any(x => x.Username == randomUserName));
        }
    }
}
