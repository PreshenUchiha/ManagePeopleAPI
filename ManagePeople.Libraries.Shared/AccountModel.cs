using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagePeople.Libraries.Shared
{
    public class AccountModel
    {
        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal OutstandingBalance { get; set; }

        // Navigation property: Account belongs to a Person
        public PersonModel? Person { get; set; }

        // Navigation property: Account has multiple transactions
        public List<TransactionModel> Transactions { get; set; } = new();
    }
}
