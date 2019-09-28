using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	class ContactMapping : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.Name)
				.IsRequired()
				.HasColumnType("varchar(200)");
			
			builder.Property(p => p.Email)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.HasMany(p => p.Phones)
				.WithOne(c => c.Contact)
				.HasForeignKey(f => f.ContactId);

			builder.HasMany(a => a.Adresses)
				.WithOne(c => c.Contact)
				.HasForeignKey(f => f.ContactId);

			builder.ToTable("Contacts");
		}
	}
}
