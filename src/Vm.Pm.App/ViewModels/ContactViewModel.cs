using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vm.Pm.App.ViewModels
{
	public class ContactViewModel
	{
		[Key]
		public Guid Id { get; set; }

		[DisplayName("Nome")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Name { get; set; }
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		[DisplayName("E-mail")]
		public string Email { get; set; }
		[DisplayName("Ativo?")]
		public bool Active { get; set; }

		[HiddenInput]
		public Guid CompanyId { get; set; }

		public CompanyViewModel Company { get; set; }
		public IEnumerable<PhoneViewModel> Phones { get; set; }
		public IEnumerable<AddressViewModel> Adresses { get; set; }
	}
}
