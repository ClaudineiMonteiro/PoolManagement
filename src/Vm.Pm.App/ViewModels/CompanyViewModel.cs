using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vm.Pm.App.ViewModels
{
	public class CompanyViewModel
	{
		[Key]
		public Guid Id { get; set; }
		
		[DisplayName("Número de Documento")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string DocumentNumber { get; set; }

		[DisplayName("FEI/EIN")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string FEIEIN { get; set; }

		[DisplayName("Nome Legal")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string LegalName { get; set; }

		[DisplayName("Nome da Marcal")]
		[Required(ErrorMessage = "O Campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string TradeName { get; set; }
		public IEnumerable<ContactViewModel> Contacts { get; set; }
		public IEnumerable<PhoneViewModel> Phones { get; set; }
		public IEnumerable<AddressViewModel> Addresses { get; set; }
	}
}
