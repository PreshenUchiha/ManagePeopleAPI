 using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;

namespace ManagePeopleApp.Frontends.MVC.Domains.Persons.Carriers
{
    public interface IPersonsClientsCarrier
    {
        public IPersonClientService PersonClientService { get; }
    }
}
