using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _99xAssessment2.Models;
using _99xAssessment2.Repository.ORM;
using Microsoft.Ajax.Utilities;

namespace _99xAssessment2.Utils
{
    public static class _99XExtensions
    {
        public static int ToInt(this string stringValue)
        {
            if (stringValue.IsNullOrWhiteSpace())
                return 0;

            return Convert.ToInt32(stringValue);
        }

        public static string ToAsOfBalance(this AccountJournal accountJournal)
        {
            if (accountJournal == null)
                return null;

            string monName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(accountJournal.Month);

            return monName + " " + accountJournal.Year;
        }


        public static IEnumerable<AccountJournalViewModel> CalculateFinalBalance(this IEnumerable<AccountJournal> journals)
        {
            if (journals == null)
                return null;

            var accountJournals = journals.ToList();

            return accountJournals.GroupBy(x => x.AccountId).Select(groupedAcc => new AccountJournalViewModel { Account = accountJournals.FirstOrDefault(x => x.AccountId == groupedAcc.Key)?.Account, SummedValues = accountJournals.Where(x => x.AccountId == groupedAcc.Key).Sum(x => x.Value) }).ToList();
        }
    }
}