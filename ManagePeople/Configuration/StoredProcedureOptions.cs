namespace ManagePeople.Configuration
{
    public class StoredProcedureOptions
    {
        public string GetAllPersons { get; set; } = string.Empty;
        public string GetPersonById { get; set; } = string.Empty;
        public string InsertNewPerson { get; set; } = string.Empty;
        public string UpdatePersonById { get; set; } = string.Empty;
        public string DeletePersonById { get; set; } = string.Empty; 

        public string GetAllAccounts { get; set; } = string.Empty;
        public string GetAllAccountsByPersonId { get; set; } = string.Empty;
        public string GetAccountById { get; set; } = string.Empty;
        public string InsertNewAccount { get; set; } = string.Empty;
        public string UpdateAccountById { get; set; } = string.Empty;
        public string DeleteAccountById { get; set; } = string.Empty;

        public string GetAllTransactionsByAccountId { get; set; } = string.Empty;
        public string GetTransactionById { get; set; } = string.Empty;
        public string InsertNewTransaction { get; set; } = string.Empty;
        public string UpdateTransactionById { get; set; } = string.Empty;
        public string DeleteTransactionById { get; set; } = string.Empty;
    }
}
