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
	public class CollaboratorService : BaseService, ICollaboratorService
	{
		private readonly ICollaboratorRepository _collaboratorRepository;
		private readonly IPhoneRepository _phoneRepository;

		public CollaboratorService(ICollaboratorRepository collaboratorRepository,
			INotifier notifier,
			IPhoneRepository phoneRepository) : base(notifier)
		{
			_collaboratorRepository = collaboratorRepository;
			_phoneRepository = phoneRepository;
		}

		public async Task Add(Collaborator collaborator)
		{
			if (!IsValid(collaborator)) return;

			await _collaboratorRepository.Add(collaborator);
		}

		public async Task Update(Collaborator collaborator)
		{
			if (!IsValid(collaborator)) return;

			await _collaboratorRepository.Update(collaborator);
		}

		public async Task Remove(Guid id)
		{
			await _collaboratorRepository.Remove(id);
		}

		public async Task AddPhone(Phone phone)
		{
			if (!PerformValidation(new PhoneValidation(), phone)) return;

			await _phoneRepository.Add(phone);
		}


		private bool IsValid(Collaborator collaborator)
		{
			bool isValid = true;
			if (!PerformValidation(new CollaboratorValidation(), collaborator)) isValid = false;

			if (_collaboratorRepository.Search(c => c.Name == collaborator.Name && c.Id != collaborator.Id).Result.Any())
			{
				Notify("Já existe um Colaborador com este nome!");
				isValid = false;
			}

			return isValid;
		}

		public void Dispose()
		{
			_collaboratorRepository?.Dispose();
			_phoneRepository?.Dispose();
		}
	}
}
