using Microsoft.EntityFrameworkCore;
using System.Linq;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Context
{
	public class PoolManagementDbContext : DbContext
	{
		public PoolManagementDbContext(DbContextOptions options) : base(options) { }

		DbSet<Address> Addresses;
		DbSet<Phone> Phones;
		DbSet<Contact> Contacts;
		DbSet<Company> Companies;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.Relational().ColumnType = "varchar(100)";

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoolManagementDbContext).Assembly);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

			base.OnModelCreating(modelBuilder);
		}
	}
}
