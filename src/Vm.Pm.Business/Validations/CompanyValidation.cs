using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class CompanyValidation : AbstractValidator<Company>
	{
		public CompanyValidation()
		{
			RuleFor(c => c.DocumentNumber)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 20).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.FEIEIN)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 20).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.LegalName)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);

			RuleFor(c => c.TradeName)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
