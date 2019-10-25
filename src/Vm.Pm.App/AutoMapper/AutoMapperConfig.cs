using AutoMapper;
using Vm.Pm.App.ViewModels;
using Vm.Pm.Business.Models;

namespace Vm.Pm.App.AutoMapper
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<Company, CompanyViewModel>().ReverseMap();
			CreateMap<Contact, ContactViewModel>().ReverseMap();
			CreateMap<Collaborator, CollaboratorViewModel>().ReverseMap();
			CreateMap<Phone, PhoneViewModel>().ReverseMap();
			CreateMap<Address, AddressViewModel>().ReverseMap();
		}
	}
}
