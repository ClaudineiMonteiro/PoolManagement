using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vm.Pm.App.ViewModels
{
	public class PhoneViewModel
	{
		[Key]
		public Guid Id { get; set; }
		[DisplayName("Número")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Number { get; set; }
		[DisplayName("Tipo de Telefone")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		public int TypePhone { get; set; }
		[HiddenInput]
		public Guid? CompanyId { get; set; }
		[HiddenInput]
		public Guid? ContactId { get; set; }
	}
}
