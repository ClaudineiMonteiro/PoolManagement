using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class CustomerMapping : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.Document)
				.HasColumnType("varchar(50)");

			builder.Property(p => p.Description)
				.HasColumnType("varchar(200)")
				.IsRequired();

			builder.Property(p => p.Email)
				.HasColumnType("varchar(20)");

			builder.HasMany(c => c.Contacts)
				.WithOne(c => c.Customer)
			.HasForeignKey(f => f.CustomerId);

			builder.HasMany(p => p.Phones)
				.WithOne(c => c.Customer)
				.HasForeignKey(f => f.CustomerId);

			builder.HasMany(a => a.Addresses)
				.WithOne(c => c.Customer)
				.HasForeignKey(f => f.CustomerId);

			builder.ToTable("Customers");
		}
	}
}
