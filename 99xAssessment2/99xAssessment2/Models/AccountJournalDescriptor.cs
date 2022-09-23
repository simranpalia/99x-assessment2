using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _99xAssessment2.Repository.ORM;

namespace _99xAssessment2.Models
{
    public class AccountJournalDescriptor
    {
        public Account Account { get; set; }

        public List<AccountJournal> AccountJournal { get; set; }
    }
}