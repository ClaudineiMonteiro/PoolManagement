using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class PhoneMapping : IEntityTypeConfiguration<Phone>
	{
		public void Configure(EntityTypeBuilder<Phone> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.Number)
				.IsRequired()
				.HasColumnType("varchar(11)");

			builder.ToTable("Phones");
		}
	}
}
