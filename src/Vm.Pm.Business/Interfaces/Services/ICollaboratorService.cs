using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface ICollaboratorService : IDisposable
	{
		Task Add(Collaborator collaborator);
		Task Update(Collaborator collaborator);
		Task Remove(Guid id);

		Task AddPhone(Phone phone);
		Task AddAddress(Address address);

		Task<Address> GetAddressById(Guid id);
		Task UpdateAddress(Address address);
		Task RemoveAddress(Guid id);
	}
}
