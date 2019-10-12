using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vm.Pm.App.ViewModels;

namespace Vm.Pm.App.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Vm.Pm.App.ViewModels.ContactViewModel> ContactViewModel { get; set; }
		public DbSet<Vm.Pm.App.ViewModels.PhoneViewModel> PhoneViewModel { get; set; }
	}
}
