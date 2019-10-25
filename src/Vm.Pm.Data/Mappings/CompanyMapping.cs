using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class CompanyMapping : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.DocumentNumber)
				.HasColumnType("varchar(20)");

			builder.Property(p => p.FEIEIN)
				.HasColumnType("varchar(20)");

			builder.Property(p => p.LegalName)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(p => p.TradeName)
				.HasColumnType("varchar(200)");

			builder.HasMany(c => c.Contacts)
				.WithOne(c => c.Company)
				.HasForeignKey(f => f.CompanyId);

			builder.HasMany(p => p.Phones)
				.WithOne(c => c.Company)
				.HasForeignKey(f => f.CompanyId);

			builder.HasMany(a => a.Addresses)
				.WithOne(c => c.Company)
				.HasForeignKey(f => f.CompanyId);

			builder.HasMany(c => c.Collaborators)
				.WithOne(c => c.Company)
				.HasForeignKey(f => f.CompanyId);

			builder.ToTable("Companies");
		}
	}
}
