namespace ManagePeopleApp.Frontends.MVC.Configuration
{
    public class ApiEndpointsConfiguration
    {
        public string GetPersonsEndpoint { get; set; } = string.Empty;
        public string GetPersonByPersonIdEndpoint { get; set; } = string.Empty;
        public string CreatePersonsEndpoint { get; set; } = string.Empty;
        public string UpdatePersonsEndpoint { get; set; } = string.Empty;
        public string DeletePersonsEndpoint { get; set; } = string.Empty;

        public string GetAccountsByPersonIdEndpoint { get; set; } = string.Empty;
        public string CreateAccountEndpoint { get; set; } = string.Empty;
        public string UpdateAccountEndpoint { get; set; } = string.Empty;
        public string DeleteAccountEndpoint { get; set; } = string.Empty;   

        public string GetTransactionsByAccountIdEndpoint { get; set; } = string.Empty;
        public string CreateTransactionEndpoint { get; set; } = string.Empty;
        public string UpdateTransactionEndpoint { get; set; } = string.Empty;
        public string DeleteTransactionEndpoint { get; set; } = string.Empty;
    }
}
