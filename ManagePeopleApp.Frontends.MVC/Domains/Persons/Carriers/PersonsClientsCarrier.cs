

using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;

namespace ManagePeopleApp.Frontends.MVC.Domains.Persons.Carriers
{
    public class PersonsClientsCarrier(
        IPersonClientService personClientService) : IPersonsClientsCarrier
    {
        public IPersonClientService PersonClientService => personClientService;
    }
}
