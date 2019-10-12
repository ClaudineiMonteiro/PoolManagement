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
	public class PhonesController : BaseController
	{
		private readonly IPhoneRepository _phoneRepository;
		private readonly IPhoneService _phoneService;
		private readonly IMapper _mapper;

		public PhonesController(IPhoneRepository phoneRepository,
			IPhoneService phoneService,
			IMapper mapper,
			INotifier notifier)
			: base(notifier)
		{
			_phoneRepository = phoneRepository;
			_phoneService = phoneService;
			_mapper = mapper;
		}

		[Route("list-of-phones")]
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<PhoneViewModel>(await _phoneRepository.GetAll()));
		}

		[Route("new-phone")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("new-phone")]
		public async Task<IActionResult> Create(PhoneViewModel phoneViewModel)
		{
			if (!ModelState.IsValid) return View(phoneViewModel);

			var phone = _mapper.Map<Phone>(phoneViewModel);

			await _phoneService.Add(phone);

			if (!ValidOperation()) return View(phoneViewModel);

			return RedirectToAction("Edit", "Contacts", new { id = phoneViewModel.Id });
		}

		[Route("data-of-phone/id:guid")]
		public async Task<IActionResult> Details(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return View(phoneViewModel);
		}

		[Route("edit-phone/id:guid")]
		public async Task<IActionResult> Edit(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return View(phoneViewModel);
		}

		[Route("edit-phone/id:guid")]
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, PhoneViewModel phoneViewModel)
		{
			if (id != phoneViewModel.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(phoneViewModel);

			var phone = _mapper.Map<Phone>(phoneViewModel);

			await _phoneService.Add(phone);

			if (!ValidOperation()) return View(_mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id)));

			return RedirectToAction("Index");
		}

		[Route("remove-phone/id:guid")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			return View(phoneViewModel);
		}

		[Route("remove-phone/id:guid")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var phoneViewModel = _mapper.Map<PhoneViewModel>(await _phoneRepository.GetById(id));

			if (phoneViewModel == null)
			{
				return NotFound();
			}

			await _phoneService.Remove(id);

			if (!ValidOperation()) return View(phoneViewModel);

			return RedirectToAction("Index");
		}
	}
}
