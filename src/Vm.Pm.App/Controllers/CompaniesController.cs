using System;
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
	public class CompaniesController : BaseController
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;

		public CompaniesController(ICompanyRepository companyRepository,
			ICompanyService companyService,
			IMapper mapper,
			INotifier notifier) : base(notifier)
		{
			_companyRepository = companyRepository;
			_companyService = companyService;
			_mapper = mapper;
		}

		[Route("list-of-companies")]
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<CompanyViewModel>>(await _companyRepository.GetAll()));
		}

		[Route("data-of-company/{id:guid}")]
		public async Task<IActionResult> Details(Guid id)
		{
			var companyViewModel = await GetCompanyContactsPhonesAddresses(id);
            
			if (companyViewModel == null)
			{
				return NotFound();
			}

			return View(companyViewModel);
		}

		[Route("new-company")]
		public IActionResult Create()
		{
			return View();
		}


		[Route("new-company")]
		[HttpPost]
		public async Task<IActionResult> Create(CompanyViewModel companyViewModel)
		{
			if (!ModelState.IsValid) return View(companyViewModel);

			var company = _mapper.Map<Company>(companyViewModel);
			
			await _companyService.Add(company);

			if (!ValidOperation()) return View(companyViewModel);

			return RedirectToAction("Index");
		}

		[Route("edit-company/{id:guid}")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var companyViewModel = await GetCompanyContactsPhonesAddresses(id);

			if (companyViewModel == null) return NotFound();

			return View(companyViewModel);
		}

		[Route("edit-company/{id:guid}")]
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, CompanyViewModel companyViewModel)
		{
			if (id != companyViewModel.Id) return NotFound();

			if (!ModelState.IsValid) return View(companyViewModel);

			var company = _mapper.Map<Company>(companyViewModel);

			await _companyService.Update(company);

			if (!ValidOperation()) return View(GetCompanyContactsPhonesAddresses(id));

			return RedirectToAction("Index");
		}

		[Route("remove-company/{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var companyViewModel = await GetCompanyContactsPhonesAddresses(id);

			if (companyViewModel == null) return NotFound();

			return View(companyViewModel);
		}

		[Route("remove-company/{id:guid}")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var companyViewModel = await GetCompanyContactsPhonesAddresses(id);

			if (companyViewModel == null) return NotFound();

			await _companyService.Remove(id);

			if (!ValidOperation()) return View(companyViewModel);

			return RedirectToAction("Index");
		}

		private async Task<CompanyViewModel> GetCompanyContactsPhonesAddresses(Guid id)
		{
			return _mapper.Map<CompanyViewModel>(await _companyRepository.GetCompanyContactsPhonesAddresses(id));
		}

	}
}
