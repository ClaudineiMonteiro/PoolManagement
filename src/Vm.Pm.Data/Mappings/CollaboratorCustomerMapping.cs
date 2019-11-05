using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Mappings
{
	public class CollaboratorCustomerMapping : IEntityTypeConfiguration<CollaboratorCustomer>
	{
		public void Configure(EntityTypeBuilder<CollaboratorCustomer> builder)
		{
			builder.HasKey(cc => new { cc.CollaboratorId, cc.CustomerId });

			builder.HasOne(cc => cc.Collaborator)
				.WithMany(c => c.CollaboratorCustomers)
				.HasForeignKey(f => f.CollaboratorId);

			builder.HasOne(cc => cc.Customer)
				.WithMany(c => c.CollaboratorCustomers)
				.HasForeignKey(f => f.CustomerId);

			builder.ToTable("CollaboratorCustomers");
		}
	}
}
