﻿using FluentValidation;
using FluentValidation.Results;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Models;
using Vm.Pm.Business.Notifications;

namespace Vm.Pm.Business.Services
{
	public abstract class BaseService
	{
		private readonly INotifier _notifier;

		protected BaseService(INotifier notifier)
		{
			_notifier = notifier;
		}

		protected void Notify(ValidationResult validationResult)
		{
			foreach (var error in validationResult.Errors)
			{
				Notify(error.ErrorMessage);
			}
		}

		protected void Notify(string message)
		{
			_notifier.Handle(new Notification(message));
		}

		protected bool PerformValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
		{
			var validator = validation.Validate(entity);

			if (validator.IsValid) return true;

			Notify(validator);

			return false;
		}
	}
}
