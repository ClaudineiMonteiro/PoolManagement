using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Vm.Pm.App.ViewModels
{
	public class PhoneViewModel
	{
		[Key]
		public Guid Id { get; set; }
		public string Number { get; set; }
		public int TypePhone { get; set; }
		[HiddenInput]
		public Guid CompanyId { get; set; }
		[HiddenInput]
		public Guid ContactId { get; set; }
	}
}
