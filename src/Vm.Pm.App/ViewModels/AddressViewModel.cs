using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vm.Pm.App.ViewModels
{
	public class AddressViewModel
	{
		[Key]
		public Guid Id { get; set; }
		
		[DisplayName("Logradouro")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string PublicPlace { get; set; }
		
		[DisplayName("Complemento")]
		public string? Apt_Suite_Unit { get; set; }
		
		[DisplayName("Cidade")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string City { get; set; }
		
		[DisplayName("Estado")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string State_Province { get; set; }
		
		[DisplayName("Cep")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string ZipPostalCode { get; set; }

		[DisplayName("Tipo")]
		public int TypeAddress { get; set; }

		[HiddenInput]
		public Guid CompanyId { get; set; }
		[HiddenInput]
		public Guid ContactId { get; set; }

	}
}
