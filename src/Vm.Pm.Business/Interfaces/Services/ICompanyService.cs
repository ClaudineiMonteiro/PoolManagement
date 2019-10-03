using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface ICompanyService :IDisposable
	{
		Task Add(Company company);
		Task Update(Company company);
		Task Remove(Guid id);
		Task UpdateContact(Contact contact);
		Task UpdatePhone(Phone phone);
		Task UpdateAddress(Address address);
	}
}
