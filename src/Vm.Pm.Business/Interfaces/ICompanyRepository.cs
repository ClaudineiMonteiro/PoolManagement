using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface ICompanyRepository : IRepository<Company>
	{
		Task<Company> GetCompanyContacts(Guid id);
		Task<Company> GetCompanyPhones(Guid id);
		Task<Company> GetCompanyAddresses(Guid id);
		Task<Company> GetCompanyContactsPhones(Guid id);
		Task<Company> GetCompanyContactsAddresses(Guid id);
		Task<Company> GetCompanyPhonesAddresses(Guid id);
		Task<Company> GetCompanyContactsPhonesAddresses(Guid id);
	}
}
