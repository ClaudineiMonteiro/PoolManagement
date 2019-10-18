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
	public class ContactService : BaseService, IContactService
	{
		private readonly IContactRepository _contactRepository;
		private readonly IPhoneRepository _phoneRepository;

		public ContactService(IContactRepository contactRepository,
			IPhoneRepository phoneRepository,
			INotifier notifier) : base(notifier)
		{
			_contactRepository = contactRepository;
			_phoneRepository = phoneRepository;
		}
		public async Task Add(Contact contact)
		{
			if (!IsValid(contact)) return;

			await _contactRepository.Add(contact);
		}

		public async Task Update(Contact contact)
		{
			if (!IsValid(contact)) return;

			await _contactRepository.Update(contact);
		}

		public async Task Remove(Guid id)
		{
			await _contactRepository.Remove(id);
		}

		private bool IsValid(Contact contact)
		{
			bool isValid = true;

			if (!PerformValidation(new ContactValidation(), contact)) isValid = false;

			if (_contactRepository.Search(c => c.Name == contact.Name && c.Id != contact.Id).Result.Any())
			{
				Notify("Já existe um contato com este nome!");
				isValid = false;
			}

			if (_contactRepository.Search(c => c.Email == contact.Email && c.Id != contact.Id).Result.Any())
			{
				Notify("Já existe um contato com este Email!");
				isValid = false;
			}

			return isValid;
		}

		public void Dispose()
		{
			_contactRepository?.Dispose();
		}

		public async Task AddPhone(Phone phone)
		{
			if (!PerformValidation(new PhoneValidation(), phone)) return;

			await _phoneRepository.Add(phone);
		}
	}
}
