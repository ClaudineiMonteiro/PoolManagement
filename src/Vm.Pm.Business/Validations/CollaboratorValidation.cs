using FluentValidation;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Validations
{
	public class CollaboratorValidation : AbstractValidator<Collaborator>
	{
		public CollaboratorValidation()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage(MessageValidation.FieldNotEmpty)
				.Length(2, 200).WithMessage(MessageValidation.FieldSizeBetweem);
		}
	}
}
