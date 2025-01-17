﻿using System.Collections.Generic;
using System.Linq;
using Vm.Pm.Business.Interfaces.Notifications;

namespace Vm.Pm.Business.Notifications
{
	public class Notifier : INotifier
	{
		private List<Notification> _notifications;
		public Notifier()
		{
			_notifications = new List<Notification>();
		}

		public List<Notification> GetNotifications()
		{
			return _notifications;
		}

		public void Handle(Notification notification)
		{
			_notifications.Add(notification);
		}

		public bool HasNotification()
		{
			return _notifications.Any();
		}
	}
}
