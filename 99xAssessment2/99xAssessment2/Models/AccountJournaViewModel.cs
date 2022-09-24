using _99xAssessment2.Repository.ORM;

namespace _99xAssessment2.Models
{
    public class AccountJournalViewModel
    {
        public Account Account { get; set; }

        public decimal SummedValues { get; set; }
    }
}