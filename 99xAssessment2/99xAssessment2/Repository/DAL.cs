using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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

        public static IEnumerable<AccountJournal> GetAccountJournals() => GetContext().AccountJournals;

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

        public static void AddOrUpdateAccountJournal(AccountJournal entity)
        {
            var ctx = GetContext();
            if (entity != null)
            {
                ctx.AccountJournals.AddOrUpdate(entity);
                ctx.SaveChanges();
            }
        }

        public static void ProcessAndUpdateAccountInfo(HttpPostedFileBase file, int year, int month, long userId)
        {
            if (file != null)
            {
                var excelResponse = ExcelUtils.ProcessExcel(file, month, year);
                if (excelResponse.Any())
                {
                    foreach (var accountJournalDescriptor in excelResponse)
                    {
                        Account accountInfo = CreateOrFindAccountInfoByName(accountJournalDescriptor.Account, userId);

                        //Add/Update account journal for Month-year-AccountId combination.
                        foreach (var accountJournal in accountJournalDescriptor.AccountJournal)
                        {
                            var matchedJournal = GetAccountJournals().FirstOrDefault(x =>
                                x.AccountId == accountInfo.Id && x.Year == year && x.Month == month);
                            if (matchedJournal == null)
                            {
                                AddOrUpdateAccountJournal(new AccountJournal
                                {
                                    AccountId = accountInfo.Id,
                                    Year = year,
                                    Month = month,
                                    Value = accountJournal.Value,
                                    InsertedAt = DateTime.Now,
                                    InsertedBy = userId
                                });
                            }
                            else
                            {
                                //Overwrite old balance values for the combination.
                                matchedJournal.Value = accountJournal.Value;
                                AddOrUpdateAccountJournal(matchedJournal);
                            }
                        }
                    }
                }
            }
        }

        private static Account CreateOrFindAccountInfoByName(Account account, long userId)
        {
            if (account != null)
            {
                var matchedInfo = GetAccounts().FirstOrDefault(x => x.AccountName == account.AccountName);
                if (matchedInfo == null)
                {
                    AddOrUpdateAccount(new Account
                    {
                        AccountName = account.AccountName,
                        InsertedAt = DateTime.Now,
                        InsertedBy = userId
                    });
                    return CreateOrFindAccountInfoByName(account, userId);
                }

                return matchedInfo;
            }

            return null;
        }
    }
}