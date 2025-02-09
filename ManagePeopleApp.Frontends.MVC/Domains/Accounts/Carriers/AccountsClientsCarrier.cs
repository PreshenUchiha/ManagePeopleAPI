using ManagePeopleApp.Frontends.MVC.Domains.Accounts.APIClient;
using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;

namespace ManagePeopleApp.Frontends.MVC.Domains.Accounts.Carriers
{
    public class AccountsClientsCarrier(
        IAccountClientService accountClientService) : IAccountClientsCarrier
    {
        public IAccountClientService AccountClientService => accountClientService;
    }
}
