namespace ManagePeople.Libraries.Shared
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property: Transaction belongs to an Account
       // public AccountModel? Account { get; set; }
    }
}
