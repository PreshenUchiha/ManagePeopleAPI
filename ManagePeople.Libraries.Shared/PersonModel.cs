namespace ManagePeople.Libraries.Shared
{
    public class PersonModel
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string IdNumber { get; set; } = string.Empty;

        // Navigation property: A person can have multiple accounts
        public List<AccountModel> Accounts { get; set; } = new();
    }
}
