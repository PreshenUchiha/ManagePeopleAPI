using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;

namespace ManagePeopleApp.Frontends.MVC.Domains.Accounts.Carriers
{
    public interface IAccountClientsCarrier
    {
        public IAccountClientService AccountClientService { get; }
    }
}
