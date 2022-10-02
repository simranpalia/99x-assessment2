using _99xAssessment2.Models;
using _99xAssessment2.Repository;
using _99xAssessment2.Repository.ORM;
using _99xAssessment2.Utils;
using Microsoft.AspNetCore.Mvc;

namespace _99XAssessment2SampleApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceOverallController : ControllerBase
    {
        [HttpGet]
        public BalanceOverall Get()
        {
            var accounts = DAL.GetAccounts();
            var accJournals = accounts.SelectMany(x => x.AccountJournals).CalculateFinalBalance();

            var toBeReturned = new BalanceOverall();

            var incr = 1;
            foreach (var accName in accounts)
            {
                if (incr == 1)
                {
                    toBeReturned.AccountType1 = SummedValues(accJournals, accName);
                }
                if (incr == 2)
                {
                    toBeReturned.AccountType2 = SummedValues(accJournals, accName);
                }
                if (incr == 3)
                {
                    toBeReturned.AccountType3 = SummedValues(accJournals, accName);
                }
                if (incr == 4)
                {
                    toBeReturned.AccountType4 = SummedValues(accJournals, accName);
                }
                if (incr == 5)
                {
                    toBeReturned.AccountType5 = SummedValues(accJournals, accName);
                }
                incr++;
            }
            return toBeReturned;
        }

        private static decimal SummedValues(IEnumerable<AccountJournalViewModel> accJournals, Account accName)
        {
            return accJournals.FirstOrDefault(x => x.Account.Id == accName.Id).SummedValues;
        }
    }
}
