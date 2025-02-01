namespace ManagePeople.Libraries.Shared
{
    public class Person
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string IdNumber { get; set; } = string.Empty;

        // Navigation property: A person can have multiple accounts
        public List<Account> Accounts { get; set; } = new();
    }
}
