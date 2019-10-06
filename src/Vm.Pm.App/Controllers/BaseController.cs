using Microsoft.AspNetCore.Mvc;
using Vm.Pm.Business.Interfaces.Notifications;

namespace Vm.Pm.App.Controllers
{
	public abstract class BaseController : Controller
	{
		private readonly INotifier _notifier;

		protected BaseController(INotifier notifier)
		{
			_notifier = notifier;
		}

		protected bool ValidOperation()
		{
			return !_notifier.HasNotification();
		}
	}
}
