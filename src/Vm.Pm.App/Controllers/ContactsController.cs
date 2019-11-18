using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
		private readonly IPhoneRepository _phoneRepository;
		private readonly IPhoneService _phoneService;
		private readonly IMapper _mapper;

		public ContactsController(IContactRepository contactRepository,
			IContactService contactService,
			IPhoneRepository phoneRepository,
			IPhoneService phoneService,
			IMapper mapper,
			INotifier notifier) : base(notifier)
		{
			_contactRepository = contactRepository;
			_contactService = contactService;
			_phoneRepository = phoneRepository;
			_phoneService = phoneService;
			_mapper = mapper;
		}

		[Route("list-of-contacts")]
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<ContactViewModel>>(await _contactRepository.GetAll()));
		}

		[Route("detail-of-contact/{id:guid}")]
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


			if (contactViewModel.CompanyId != null)
			{
				return RedirectToAction("Edit", "Companies", new { id = contactViewModel.CompanyId }); 
			}
			else
			{
				return RedirectToAction("Edit", "Customers", new { id = contactViewModel.CustomerId });
			}
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

		[Route("add-contact-by-list-of-collaborator/{id:guid}")]
		public ActionResult AddContactByListCollaborator(Guid id)
		{
			return View("Create", new ContactViewModel { CollaboratorId = id });
		}

		[Route("add-contact-by-list-of-customer/{id:guid}")]
		public ActionResult AddContactByListCustomer(Guid id)
		{
			return View("Create", new ContactViewModel { CustomerId = id });
		}
		#region Phone
		[AllowAnonymous]
		[Route("get-phone-contact/{id:guid}")]
		public async Task<IActionResult> GetPhone(Guid id)
		{
			var phones = await GetPhonesContact(id);

			if (phones == null)
			{
				return NotFound();
			}

			return PartialView("_PhoneList", phones);
		}

		[AllowAnonymous]
		[Route("get-phones-contact/{id:guid}")]
		public async Task<IActionResult> GetPhonesContact(Guid id)
		{

			var contactViewModel = _mapper.Map<ContactViewModel>(await _contactRepository.GetContactPhonesAddresses(id));

			if (contactViewModel == null)
			{
				return NotFound();
			}

			return PartialView("_PhonesList", contactViewModel);
		}

		[Route("new-phone-contact/{id:guid}")]
		public IActionResult NewPhone(Guid id)
		{
			return PartialView("_AddPhone", new PhoneViewModel { ContactId = id });
		}

		[Route("addnew-phone-contact")]
		[HttpPost]
		public async Task<IActionResult> AddNewPhone(PhoneViewModel phoneViewModel)
		{
			if (!ModelState.IsValid) return PartialView("_PhoneList", phoneViewModel);

			await _contactService.AddPhone(_mapper.Map<Phone>(phoneViewModel));

			if (!ValidOperation()) return PartialView("_AddPhone", phoneViewModel);

			var url = Url.Action("GetPhonesContact", "Contacts", new { id = phoneViewModel.ContactId });
			return Json(new { success = true, url });
		}

		[Route("delete-phone-contact/{id:guid}")]
		public async Task<IActionResult> DeletePhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("_DeletePhone", phoneViewModel);
		}

		[Route("confirme-delete-phone-contact")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePhone(PhoneViewModel phoneViewModel)
		{

			await _phoneRepository.Remove(phoneViewModel.Id);

			if (!ValidOperation()) return PartialView("_DeletePhone", phoneViewModel);

			var url = Url.Action("GetPhonesContact", "Contacts", new { id = phoneViewModel.ContactId });
			return Json(new { success = true, url });
		}

		[Route("edit-phone-contact/{id:guid}")]
		public async Task<IActionResult> EditPhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("_EditPhone", phoneViewModel);
		}

		[Route("confirme-edit-phone-contact")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPhone(PhoneViewModel phoneViewModel)
		{
			if (!ModelState.IsValid) return PartialView("_EditPhone", phoneViewModel);

			await _phoneService.Update(_mapper.Map<Phone>(phoneViewModel));

			if (!ValidOperation()) return PartialView("_EditPhone", phoneViewModel);

			var url = Url.Action("GetPhonesContact", "Contacts", new { id = phoneViewModel.ContactId });
			return Json(new { success = true, url });
		}

		[Route("detail-phone-contact/{id:guid}")]
		public async Task<IActionResult> DetailPhone(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return PartialView("_DetailPhone", phoneViewModel);
		} 
		#endregion
	}
}
