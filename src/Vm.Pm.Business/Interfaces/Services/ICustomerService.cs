using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface ICustomerService : IDisposable
	{
		Task Add(Customer customer);
		Task Update(Customer customer);
		Task Remove(Guid id);

		Task<Phone> GetPhoneById(Guid id);
		Task AddPhone(Phone phone);
		Task UpdatePhone(Phone phone);
		Task RemovePhone(Guid id);

		Task AddAddress(Address address);
		Task<Address> GetAddressById(Guid id);
		Task UpdateAddress(Address address);
		Task RemoveAddress(Guid id);
	}
}
