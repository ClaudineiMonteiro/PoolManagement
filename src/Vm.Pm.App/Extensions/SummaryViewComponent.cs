﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces.Notifications;

namespace Vm.Pm.App.Extensions
{
	public class SummaryViewComponent : ViewComponent
	{
		private readonly INotifier _notifier;

		public SummaryViewComponent(INotifier notifier)
		{
			_notifier = notifier;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var notifications = await Task.FromResult(_notifier.GetNotifications());

			notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));

			return View();
		}
	}
}
