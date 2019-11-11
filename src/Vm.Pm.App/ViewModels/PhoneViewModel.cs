using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vm.Pm.Business.Enumerators;

namespace Vm.Pm.App.ViewModels
{
	public class PhoneViewModel
	{
		public PhoneViewModel()
		{
			FillTypePhone();
		}

		[Key]
		public Guid Id { get; set; }
		[DisplayName("Número")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Number { get; set; }
		[DisplayName("Tipo de Telefone")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		public int TypePhoneId { get; set; }
		[HiddenInput]
		public Guid? CompanyId { get; set; }
		[HiddenInput]
		public Guid? ContactId { get; set; }
		[HiddenInput]
		public Guid? CollaboratorId { get; set; }
		[HiddenInput]
		public Guid? CustomerId { get; set; }

		public List<TypePhone> TypePhones { get; set; }

		private void FillTypePhone()
		{
			TypePhones = new List<TypePhone>();

			foreach (var typePhone in Enum.GetValues(typeof(Business.Enumerators.TypePhone)))
			{
				TypePhones.Add(new ViewModels.TypePhone { Id = (int)typePhone, Description = typePhone.ToString() });
			}
		}
	}

	public class TypePhone
	{
		public int Id { get; set; }
		public string Description { get; set; }
	}
}
