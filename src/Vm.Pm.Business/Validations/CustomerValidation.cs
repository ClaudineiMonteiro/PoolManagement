using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class CustomerValidation : AbstractValidator<Customer>
	{
		public CustomerValidation()
		{
			RuleFor(c => c.Description)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.Email)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
