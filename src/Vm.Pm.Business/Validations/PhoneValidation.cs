using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class PhoneValidation : AbstractValidator<Phone>
	{
		public PhoneValidation()
		{
			RuleFor(p => p.Number)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 10).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
