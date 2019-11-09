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
	public class CustomersController : BaseController
	{
		#region Attributes
		private readonly ICustomerService _customerService;
		private readonly ICustomerRepository _customerRepository;
		private readonly ICollaboratorRepository _collaboratorRepository;
		private readonly IMapper _mapper;

		#endregion
		#region Builders
		public CustomersController(ICustomerService customerService,
			ICustomerRepository customerRepository,
			ICollaboratorRepository collaboratorRepository,
			IMapper mapper,
			INotifier notifier) : base(notifier)
		{
			_customerService = customerService;
			_customerRepository = customerRepository;
			_collaboratorRepository = collaboratorRepository;
			_mapper = mapper;
		}

		#endregion

		#region Methods

		[Route("list-of-customers")]
		public async Task<IActionResult> Index()
		{
			var customersViewModel = _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAll());
			return View(customersViewModel);
		}

		[Route("details-of-customer/{id:guid}")]
		public async Task<IActionResult> Details(Guid id)
		{
			var customerViewModel = _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));

			if (customerViewModel == null)
			{
				return NotFound();
			}

			return View(customerViewModel);
		}

		[Route("new-customer")]
		public async Task<IActionResult> Create()
		{
			var customerViewmodel = await FillCollaborators(new CustomerViewModel());
			return View(customerViewmodel);
		}

		[Route("new-customer")]
		[HttpPost]
		public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
		{
			customerViewModel = await FillCollaborators(new CustomerViewModel());

			if (ModelState.IsValid) return View(customerViewModel);

			var customer = _mapper.Map<Customer>(customerViewModel);

			await _customerService.Add(customer);

			if (!ValidOperation()) return View(customerViewModel);

			return View("Index");
		}

		[Route("edit-customer/{id:guid}")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var customerViewModel = _mapper.Map<CustomerViewModel>(await _customerRepository.GetCustomerPhonesAddressesContactsCollaborators(id));

			if (customerViewModel == null)
			{
				return NotFound();
			}

			customerViewModel = await FillCollaborators(customerViewModel);

			return View(customerViewModel);
		}

		[Route("edit-customer/{id:guid}")]
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, CustomerViewModel customerViewModel)
		{

			if (id != customerViewModel.Id) return NotFound();

			if (!ModelState.IsValid) return View(customerViewModel);

			var curstomer = _mapper.Map<Customer>(customerViewModel);

			await _customerService.Update(curstomer);

			if (!ValidOperation()) return View(_mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id)));

			return View("Index");
		}

		[Route("remove-customer/{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var customerViewModel = _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));

			if (customerViewModel == null) return NotFound();

			
			return View(customerViewModel);
		}

		[Route("remove-customer/{id:guid}")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var customerViewModel = _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));

			if (customerViewModel == null) return NotFound();

			await _customerService.Remove(id);

			if (!ValidOperation()) return View(customerViewModel);

			return RedirectToAction("Index");
		}

		private async Task<CustomerViewModel> FillCollaborators(CustomerViewModel customerViewModel)
		{
			var collaborators = _mapper.Map<IEnumerable<CollaboratorViewModel>>(await _collaboratorRepository.GetAll());
			customerViewModel.Collaborators = collaborators;
			return customerViewModel;

		}
	}
	#endregion
}
