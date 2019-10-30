using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vm.Pm.App.Data;
using Vm.Pm.App.ViewModels;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Interfaces.Services;
using Vm.Pm.Business.Models;

namespace Vm.Pm.App.Controllers
{
	public class CollaboratorsController : BaseController
	{
		#region Attributes
		private readonly ICollaboratorService _collaboratorService;
		private readonly ICollaboratorRepository _collaboratorRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly IPhoneRepository _phoneRepository;
		private readonly IPhoneService _phoneService;
		private readonly IMapper _mapper;
		#endregion

		#region Builders
		public CollaboratorsController(ICollaboratorService collaboratorService,
	ICollaboratorRepository collaboratorRepository,
	ICompanyRepository companyRepository,
	IPhoneRepository phoneRepository,
	IPhoneService phoneService,
	IMapper mapper,
	INotifier notifier) : base(notifier)
		{
			_collaboratorService = collaboratorService;
			_collaboratorRepository = collaboratorRepository;
			_companyRepository = companyRepository;
			_phoneRepository = phoneRepository;
			_phoneService = phoneService;
			_mapper = mapper;
		}

		#endregion

		#region Methods
		[Route("list-of-collaborators")]
		public async Task<IActionResult> Index()
		{
			var collaboratorsViewModel = _mapper.Map<IEnumerable<CollaboratorViewModel>>(await _collaboratorRepository.GetAll());
			return View(collaboratorsViewModel);
		}

		[Route("details-of-collaborator/{id:guid}")]
		public async Task<IActionResult> Details(Guid id)
		{
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetById(id));

			if (collaboratorViewModel == null)
			{
				return NotFound();
			}

			return View(collaboratorViewModel);
		}

		[Route("new-collaborator")]
		public async Task<IActionResult> Create()
		{
			var collaboratorViewModel = await FillCollaborator(new CollaboratorViewModel());
			return View(collaboratorViewModel);
		}

		private async Task<CollaboratorViewModel> FillCollaborator(CollaboratorViewModel collaboratorViewModel)
		{
			var companies = _mapper.Map<IEnumerable<CompanyViewModel>>(await _companyRepository.GetAll());
			collaboratorViewModel.Companies = companies;
			return collaboratorViewModel;
		}

		[Route("new-collaborator")]
		[HttpPost]
		public async Task<IActionResult> Create(CollaboratorViewModel collaboratorViewModel)
		{
			collaboratorViewModel = await FillCollaborator(collaboratorViewModel);

			if (!ModelState.IsValid) return View(collaboratorViewModel);

			var collaborator = _mapper.Map<Collaborator>(collaboratorViewModel);

			await _collaboratorService.Add(collaborator);

			if (!ValidOperation()) return View(collaboratorViewModel);

			return RedirectToAction("Index");
		}

		[Route("edit-collaborator/{id:guid}")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetCollaboratorPhonesAddresses(id));

			if (collaboratorViewModel == null)
			{
				return NotFound();
			}

			collaboratorViewModel = await FillCollaborator(collaboratorViewModel);

			return View(collaboratorViewModel);
		}

		[Route("edit-collaborator/{id:guid}")]
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, CollaboratorViewModel collaboratorViewModel)
		{
			if (id != collaboratorViewModel.Id) return NotFound();

			if (!ModelState.IsValid) return View(collaboratorViewModel);

			var collaborator = _mapper.Map<Collaborator>(collaboratorViewModel);

			await _collaboratorService.Update(collaborator);

			if (!ValidOperation()) return View(_mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetById(id)));

			return RedirectToAction("Index");
		}

		[Route("remove-collaborator/{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetById(id));

			if (collaboratorViewModel == null) return NotFound();

			return View(collaboratorViewModel);
		}

		[Route("remove-collaborator/{id:guid}")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetById(id));

			if (collaboratorViewModel == null) return NotFound();

			await _collaboratorService.Remove(id);

			if (!ValidOperation()) return View(collaboratorViewModel);

			return RedirectToAction("Index");
		}

		#endregion

		#region Phone
		[AllowAnonymous]
		[Route("get-phones-collaborator/{id:guid}")]
		public async Task<IActionResult> GetPhonesCollaborator(Guid id)
		{
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetCollaboratorPhonesAddresses(id));

			if (collaboratorViewModel == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Collaborator/_PhonesListCollaborator.cshtml", collaboratorViewModel);
		}

		[Route("new-phone-collaborator/{id:guid}")]
		public IActionResult NewPhone(Guid id)
		{
			return PartialView("~/Views/Shared/Phone/_AddPhone.cshtml", new PhoneViewModel { CollaboratorId = id });
		}

		[Route("addnew-phone-collaborator")]
		[HttpPost]
		public async Task<IActionResult> AddNewPhone(PhoneViewModel phoneViewModel)
		{
			if (!ModelState.IsValid) return PartialView("~/Views/Shared/Phone/_PhoneList", phoneViewModel);

			await _collaboratorService.AddPhone(_mapper.Map<Phone>(phoneViewModel));

			if (!ValidOperation()) return PartialView("~/Views/Shared/Phone/_AddPhone", phoneViewModel);

			var url = Url.Action("GetPhonesCollaborator", "Collaborator", new { id = phoneViewModel.CollaboratorId });
			return Json(new { success = true, url });
		}

		[Route("detail-phone-collaborator/{id:guid}")]
		public async Task<IActionResult> DetailPhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Shared/Phone/_DetailPhone.cshtml", phoneViewModel);
		}

		[Route("edit-phone-collaborator/{id:guid}")]
		public async Task<IActionResult> EditPhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Shared/Phone/_EditPhone.cshtml", phoneViewModel);
		}

		[Route("save-edit-phone-collaborator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SaveEditPhone(PhoneViewModel phoneViewModel)
		{
			if (!ModelState.IsValid) return PartialView("~/Views/Shared/Phone/_EditPhone.cshtml", phoneViewModel);

			await _phoneService.Update(_mapper.Map<Phone>(phoneViewModel));

			if (!ValidOperation()) return PartialView("~/Views/Shared/Phone/_EditPhone.cshtml", phoneViewModel);

			var url = Url.Action("GetPhonesCollaborator", "Collaborator", new { id = phoneViewModel.CollaboratorId });
			return Json(new { success = true, url });
		}

		[Route("delete-phone-collaborator/{id:guid}")]
		public async Task<IActionResult> DeletePhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Shared/Phone/_DeletePhone.cshtml", phoneViewModel);
		}

		[Route("confirme-delete-phone-collaborator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ConfirmeDeletePhone(PhoneViewModel phoneViewModel)
		{

			await _phoneRepository.Remove(phoneViewModel.Id);

			if (!ValidOperation()) return PartialView("~/Views/Shared/Phone/_DeletePhone.cshtml", phoneViewModel);

			var url = Url.Action("GetPhonesCollaborator", "Collaborator", new { id = phoneViewModel.CollaboratorId });
			return Json(new { success = true, url });
		} 
		#endregion
	}
}
