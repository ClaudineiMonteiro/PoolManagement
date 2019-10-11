using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class ContactValidation : AbstractValidator<Contact>
	{
		public ContactValidation()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.Email)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
