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
	public class CustomerService : BaseService, ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IPhoneService _phoneService;
		private readonly IPhoneRepository _phoneRepository;
		private readonly IAddressService _addressService;
		private readonly IAddressRepository _addressRepository;
		private readonly ICollaboratorService _collaboratorService;

		public CustomerService(ICustomerRepository customerRepository,
			IPhoneService phoneService,
			IPhoneRepository phoneRepository,
			IAddressService addressService,
			IAddressRepository addressRepository,
			ICollaboratorService collaboratorService,
			INotifier notifier) : base(notifier)
		{
			_customerRepository = customerRepository;
			_phoneRepository = phoneRepository;
			_phoneService = phoneService;
			_addressService = addressService;
			_addressRepository = addressRepository;
			_collaboratorService = collaboratorService;
		}

		public async Task Add(Customer customer)
		{
			if (!IsValid(customer)) return;

			await _customerRepository.Add(customer);
		}
		public async Task Update(Customer customer)
		{
			if (!IsValid(customer)) return;

			await _customerRepository.Update(customer);
		}

		public async Task Remove(Guid id)
		{
			var customer = _customerRepository.GetCustomerPhonesAddressesContactsCollaborators(id);

			//foreach (var phone in customer.Result.Phones)
			//{
			//	await _phoneService.Remove(phone.Id);
			//}

			//foreach (var address in customer.Result.Addresses)
			//{
			//	await _addressService.Remove(address.Id);
			//}

			await _customerRepository.Remove(id);
		}

		private bool IsValid(Customer customer)
		{
			bool isValid = true;
			if (!PerformValidation(new CustomerValidation(), customer)) isValid = false;

			if (_customerRepository.Search(c => c.Description == customer.Description && c.Id != customer.Id).Result.Any())
			{
				Notify("Já existe um Customer com esta descrição!");
				isValid = false;
			}

			return isValid;
		}

		#region Address
		public async Task AddAddress(Address address)
		{
			if (!PerformValidation(new AddressValidation(), address)) return;

			await _addressService.Add(address);
		}

		public async Task<Address> GetAddressById(Guid id)
		{
			return await _addressRepository.GetById(id);
		}

		public async Task UpdateAddress(Address address)
		{
			if (!PerformValidation(new AddressValidation(), address)) return;

			await _addressService.Update(address);
		}

		public async Task RemoveAddress(Guid id)
		{
			await _addressService.Remove(id);
		}
		#endregion

		#region Phone
		public async Task AddPhone(Phone phone)
		{
			if (!PerformValidation(new PhoneValidation(), phone)) return;

			await _phoneService.Add(phone);
		}

		public async Task<Phone> GetPhoneById(Guid id)
		{
			return await _phoneRepository.GetById(id);
		}

		public async Task UpdatePhone(Phone phone)
		{
			if (!PerformValidation(new PhoneValidation(), phone)) return;

			await _phoneService.Update(phone);
		}

		public async Task RemovePhone(Guid id)
		{
			await _phoneService.Remove(id);
		}
		#endregion

		public void Dispose()
		{
			_phoneService?.Dispose();
			_phoneRepository?.Dispose();
			_addressService?.Dispose();
			_addressRepository?.Dispose();
			_collaboratorService?.Dispose();
			_customerRepository?.Dispose();
		}
	}
}
