using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vm.Pm.App.ViewModels;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Interfaces.Services;
using Vm.Pm.Business.Models;

namespace Vm.Pm.App.Controllers
{
	public class ContactsController : BaseController
	{
		private readonly IContactRepository _contactRepository;
		private readonly IContactService _contactService;
		private readonly IMapper _mapper;

		public ContactsController(IContactRepository contactRepository,
			IContactService contactService,
			IMapper mapper,
			INotifier notifier) : base(notifier)
		{
			_contactRepository = contactRepository;
			_contactService = contactService;
			_mapper = mapper;
		}

		[Route("list-of-contacts")]
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<ContactViewModel>(_contactRepository.GetAll()));
		}

		[Route("data-of-contact/{id:guid}")]
		public async Task<IActionResult> Details(Guid id)
		{
			var contactViewModel = _mapper.Map<ContactViewModel>(await _contactRepository.GetById(id));

			if (contactViewModel == null)
			{
				return NotFound();
			}

			return View(contactViewModel);
		}

		[Route("new-contact")]
		public IActionResult Create()
		{
			return View();
		}

		[Route("new-contact")]
		[HttpPost]
		public async Task<IActionResult> Create(ContactViewModel contactViewModel)
		{
			if (!ModelState.IsValid) return View(contactViewModel);

			var contact = _mapper.Map<Contact>(contactViewModel);

			await _contactService.Add(contact);

			if (!ValidOperation()) return View(contactViewModel);

			return RedirectToAction("Edit", "Companies", new { id = contactViewModel.CompanyId });
		}

		[Route("edit-contact/{id:guid}")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var contactViewModel = _mapper.Map<ContactViewModel>(await _contactRepository.GetContactPhonesAddresses(id));

			if (contactViewModel == null)
			{
				return NotFound();
			}

			return View(contactViewModel);
		}
		[Route("edit-contact/{id:guid}")]
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, ContactViewModel contactViewModel)
		{
			if (id != contactViewModel.Id) return NotFound();

			if (!ModelState.IsValid) return View(contactViewModel);

			var contact = _mapper.Map<Contact>(contactViewModel);

			await _contactService.Update(contact);

			if (!ValidOperation()) return View(_mapper.Map<ContactViewModel>(await _contactRepository.GetById(id)));

			return RedirectToAction("Index");
		}

		[Route("remove-contact/{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var contactViewModel = _mapper.Map<ContactViewModel>(await _contactRepository.GetById(id));

			if (contactViewModel == null)
			{
				return NotFound();
			}

			return View(contactViewModel);
		}

		[Route("remove-contact/{id:guid}")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var contactViewModel = _mapper.Map<ContactViewModel>(await _contactRepository.GetById(id));

			if (contactViewModel == null)
			{
				return NotFound();
			}

			await _contactService.Remove(id);

			if (!ValidOperation()) return View(contactViewModel);

			return RedirectToAction("Index");
		}

		[Route("add-contact-by-list/{id:guid}")]
		public ActionResult AddContactByList(Guid id)
		{
			return View("Create", new ContactViewModel { CompanyId = id });
		}
	}
}
