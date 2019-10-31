using System;
using System.Linq;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Interfaces.Services;
using Vm.Pm.Business.Models;
using Vm.Pm.Business.Validations;

namespace Vm.Pm.Business.Services
{
	public class AddressService : BaseService, IAddressService
	{
		private readonly IAddressRepository _addressRepository;

		public AddressService(IAddressRepository addressRepository,
			INotifier notifier) : base(notifier)
		{
			_addressRepository = addressRepository;
		}
		public async Task Add(Address address)
		{
			if (!IsValid(address)) return;

			await _addressRepository.Add(address);
		}

		public async Task Update(Address address)
		{
			if (!IsValid(address)) return;

			await _addressRepository.Update(address);
		}

		public async Task Remove(Guid id)
		{
			await _addressRepository.Remove(id);
		}

		private bool IsValid(Address address)
		{
			bool isValid = true;

			if (!PerformValidation(new AddressValidation(), address)) isValid = false;

			if (_addressRepository.Search(p => p.PublicPlace == address.PublicPlace && p.City == address.City).Result.Any())
			{
				Notify("Este Address já esta cadastrado!");
				isValid = false;
			}

			return isValid; ;
		}

		public void Dispose()
		{
			_addressRepository?.Dispose();
		}
	}
}
