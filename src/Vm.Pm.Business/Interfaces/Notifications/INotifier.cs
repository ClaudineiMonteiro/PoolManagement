using System.Collections.Generic;
using Vm.Pm.Business.Notifications;

namespace Vm.Pm.Business.Interfaces.Notifications
{
	public interface INotifier
	{
		bool HasNotification();
		List<Notification> GetNotifications();
		void Handle(Notification notification);
	}
}
