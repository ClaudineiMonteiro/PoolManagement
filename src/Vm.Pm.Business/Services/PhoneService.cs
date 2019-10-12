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
	public class PhoneService : BaseService, IPhoneService
	{
		private readonly IPhoneRepository _phoneRepository;

		public PhoneService(IPhoneRepository phoneRepository,
			INotifier notifier)
			: base(notifier)
		{
			_phoneRepository = phoneRepository;
		}
		public async Task Add(Phone phone)
		{
			if (!IsValid(phone)) return;

			await _phoneRepository.Add(phone);
		}

		public async Task Update(Phone phone)
		{
			if (!IsValid(phone)) return;

			await _phoneRepository.Update(phone);
		}

		public async Task Remove(Guid id)
		{
			_phoneRepository.Remove(id);
		}

		private bool IsValid(Phone phone)
		{
			bool isValid = true;
			if (!PerformValidation(new PhoneValidation(), phone)) isValid = false;

			if (_phoneRepository.Search(p => p.Number == phone.Number).Result.Any())
			{
				Notify("Este telefone já esta cadastrado!");
				isValid = false;
			}

			return isValid;
		}

		public void Dispose()
		{
			_phoneRepository?.Dispose();
		}
	}
}
