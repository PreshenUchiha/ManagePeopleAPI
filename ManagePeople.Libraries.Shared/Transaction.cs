namespace ManagePeople.Libraries.Shared
{
    public class Transaction
    {
        public int Code { get; set; }
        public int AccountCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property: Transaction belongs to an Account
        public Account? Account { get; set; }
    }
}
