using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class AddressMapping : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.PublicPlace)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(p => p.Apt_Suite_Unit)
				.HasColumnType("varchar(20)");

			builder.Property(p => p.City)
				.IsRequired()
				.HasColumnType("varchar(20)");

			builder.Property(p => p.State_Province)
				.IsRequired()
				.HasColumnType("varchar(20)");

			builder.Property(p => p.ZipPostalCode)
				.IsRequired()
				.HasColumnType("varchar(8)");

			builder.ToTable("Adresses");
		}
	}
}
