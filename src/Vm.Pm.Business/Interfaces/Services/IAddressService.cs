using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface IAddressService : IDisposable
	{
		Task Add(Address address);
		Task Update(Address address);
		Task Remove(Guid id);
	}
}
