using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces.Services
{
	public interface IContactService : IDisposable
	{
		Task Add(Contact contact);
		Task Update(Contact contact);
		Task Remove(Guid id);

		Task AddPhone(Phone phone);
	}
}
