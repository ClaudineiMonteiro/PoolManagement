﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly ICollaboratorService _collaboratorService;
		private readonly ICollaboratorRepository _collaboratorRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CollaboratorsController(ICollaboratorService collaboratorService,
			ICollaboratorRepository collaboratorRepository,
			ICompanyRepository companyRepository,
			IMapper mapper,
			INotifier notifier) : base(notifier)
		{
			_collaboratorService = collaboratorService;
			_collaboratorRepository = collaboratorRepository;
			_companyRepository = companyRepository;
			_mapper = mapper;
		}

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
			var collaboratorViewModel = _mapper.Map<CollaboratorViewModel>(await _collaboratorRepository.GetById(id));

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
	}
}
