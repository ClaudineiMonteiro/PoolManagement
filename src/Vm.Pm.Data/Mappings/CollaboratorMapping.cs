using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class CollaboratorMapping : IEntityTypeConfiguration<Collaborator>
	{
		public void Configure(EntityTypeBuilder<Collaborator> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.Name)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.HasMany(p => p.Phones)
				.WithOne(c => c.Collaborator)
				.HasForeignKey(f => f.CollaboratorId);

			builder.HasMany(a => a.Addresses)
				.WithOne(c => c.Collaborator)
				.HasForeignKey(f => f.CollaboratorId);

			builder.ToTable("Collaborators");
		}
	}
}
