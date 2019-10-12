using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface IPhoneService : IDisposable
	{
		Task Add(Phone phone);
		Task Update(Phone phone);
		Task Remove(Guid id);
	}
}
