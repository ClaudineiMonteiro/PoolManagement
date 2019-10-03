using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Interfaces.Services;
using Vm.Pm.Business.Models;
using Vm.Pm.Business.Validations;

namespace Vm.Pm.Business.Services
{
	public class CompanyService : BaseService, ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IContactRepository _contactRepository;
		private readonly IPhoneRepository _phoneRepository;
		private readonly IAddressRepository _addressRepository;

		public CompanyService(ICompanyRepository companyRepository,
			IContactRepository contactRepository,
			IPhoneRepository phoneRepository,
			IAddressRepository addressRepository,
			INotifier notifier)
			: base(notifier)
		{
			_companyRepository = companyRepository;
			_contactRepository = contactRepository;
			_phoneRepository = phoneRepository;
			_addressRepository = addressRepository;
		}
		public async Task Add(Company company)
		{
			if (!PerformValidation(new CompanyValidation(), company)) return;

			if (!ValidateContacts(company.Contacts)) return;

			if (!ValidationPhones(company.Phones)) return;

			if (!ValidationAddress(company.Addresses)) return;

			if (_companyRepository.Search(c => c.DocumentNumber == company.DocumentNumber).Result.Any())
			{
				Notify("Já existe uma Compania com este Número de Documento");
				return;
			}

			if (_companyRepository.Search(c => c.FEIEIN == company.FEIEIN).Result.Any())
			{
				Notify("Já existe uma Compania com este FEI ou EIN");
				return;
			}

			await _companyRepository.Add(company);
		}

		public async Task Update(Company company)
		{
			if (!PerformValidation(new CompanyValidation(), company)) return;

			if (_companyRepository.Search(c => c.DocumentNumber == company.DocumentNumber && c.Id != company.Id).Result.Any())
			{
				Notify("Já existe uma Compania com este Número de Documento");
				return;
			}

			if (_companyRepository.Search(c => c.FEIEIN == company.FEIEIN && c.Id != company.Id).Result.Any())
			{
				Notify("Já existe uma Compania com este FEI ou EIN");
				return;
			}

			await _companyRepository.Update(company);
		}

		public async Task UpdateAddress(Address address)
		{
			if (!PerformValidation(new AddressValidation(), address)) return;

			await _addressRepository.Update(address);
		}

		public async Task UpdateContact(Contact contact)
		{
			if (!PerformValidation(new ContactValidation(), contact)) return;

			await _contactRepository.Update(contact);
		}

		public async Task UpdatePhone(Phone phone)
		{
			if (!PerformValidation(new PhoneValidation(), phone)) return;

			await _phoneRepository.Update(phone);
		}

		public async Task Remove(Guid id)
		{
			await RemoveContacts(id);

			await RemovePhones(id);

			await RemoveAddresses(id);

			await _companyRepository.Remove(id);
		}

		private async Task RemoveAddresses(Guid id)
		{
			var addresses = await _addressRepository.GetAddressesByCompany(id);

			foreach (var address in addresses)
			{
				await _addressRepository.Remove(address.Id);
			}
		}

		private async Task RemovePhones(Guid id)
		{
			var phones = await _phoneRepository.GetPhonesByCompany(id);

			foreach (var phone in phones)
			{
				await _phoneRepository.Remove(phone.Id);
			}
		}

		private async Task RemoveContacts(Guid id)
		{
			var contacts = await _contactRepository.GetContactsByCompany(id);

			foreach (var contact in contacts)
			{
				await _contactRepository.Remove(contact.Id);
			}
		}

		private bool ValidationAddress(IEnumerable<Address> addresses)
		{
			bool addressValid = true;
			foreach (var address in addresses)
			{
				if (!PerformValidation(new AddressValidation(), address)) addressValid = false;
			}

			return addressValid;
		}

		private bool ValidationPhones(IEnumerable<Phone> phones)
		{
			bool phoneValid = true;
			foreach (var phone in phones)
			{
				if (!PerformValidation(new PhoneValidation(), phone)) phoneValid = false;
			}

			return phoneValid;
		}

		private bool ValidateContacts(IEnumerable<Contact> contacts)
		{
			bool contactValid = true;
			foreach (var contact in contacts)
			{
				if (!PerformValidation(new ContactValidation(), contact)) contactValid = false;
			}

			return contactValid;
		}

		public void Dispose()
		{
			_companyRepository?.Dispose();
			_contactRepository?.Dispose();
			_phoneRepository?.Dispose();
			_addressRepository?.Dispose();
		}
	}
}
