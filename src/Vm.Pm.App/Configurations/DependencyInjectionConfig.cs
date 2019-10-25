using Microsoft.Extensions.DependencyInjection;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Interfaces.Notifications;
using Vm.Pm.Business.Interfaces.Services;
using Vm.Pm.Business.Notifications;
using Vm.Pm.Business.Services;
using Vm.Pm.Data.Context;
using Vm.Pm.Data.Repository;

namespace Vm.Pm.App.Configurations
{
	public static class DependencyInjectionConfig
	{
		public static IServiceCollection ResolveDependencies(this IServiceCollection services)
		{
			services.AddScoped<PoolManagementDbContext>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<IPhoneRepository, PhoneRepository>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();

			services.AddScoped<INotifier, Notifier>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<IContactService, ContactService>();
			services.AddScoped<IPhoneService, PhoneService>();
			services.AddScoped<ICollaboratorService, CollaboratorService>();

			return services;
		}
	}
}
