using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class ContactValidation : AbstractValidator<Contact>
	{
		public ContactValidation()
		{
			RuleFor(c => c.Name)
				.Empty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.Email)
				.Empty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
