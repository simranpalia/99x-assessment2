using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Web;
using _99xAssessment2.Repository.ORM;
using _99xAssessment2.Utils;

namespace _99xAssessment2.Repository
{
    public class DAL : Entities
    {
        public static Entities GetContext()
        {
            return new Entities();
        }

        public static IEnumerable<User> GetUsers() => GetContext().Users;

        public static IEnumerable<Account> GetAccounts() => GetContext().Accounts;

        public static void AddOrUpdateUser(User user)
        {
            var ctx = GetContext();
            if (user != null)
            {
                ctx.Users.AddOrUpdate(user);
                ctx.SaveChanges();
            }
        }

        public static void AddOrUpdateAccount(Account entity)
        {
            var ctx = GetContext();
            if (entity != null)
            {
                ctx.Accounts.AddOrUpdate(entity);
                ctx.SaveChanges();
            }
        }

        public static Tuple<bool, string> ProcessAndUpdateAccountInfo(HttpPostedFileBase file, int year, int month, long userId)
        {
            if (file != null)
            {
                var excelResponse = ExcelUtils.ProcessExcel(file, month, year);
            }

            return new Tuple<bool, string>(false, "File can't be null");
        }
    }
}