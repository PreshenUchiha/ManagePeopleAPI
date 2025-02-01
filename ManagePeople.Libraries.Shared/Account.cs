using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagePeople.Libraries.Shared
{
    public class Account
    {
        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal OutstandingBalance { get; set; }

        // Navigation property: Account belongs to a Person
        public Person? Person { get; set; }

        // Navigation property: Account has multiple transactions
        public List<Transaction> Transactions { get; set; } = new();
    }
}
