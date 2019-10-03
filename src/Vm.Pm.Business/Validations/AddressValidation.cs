using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class AddressValidation : AbstractValidator<Address>
	{
		public AddressValidation()
		{
			RuleFor(a => a.PublicPlace)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(a => a.City)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 20).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(a => a.State_Province)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 20).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(a => a.ZipPostalCode)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 8).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
