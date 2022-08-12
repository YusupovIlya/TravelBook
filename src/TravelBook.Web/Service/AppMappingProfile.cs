using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TravelBook.Web.ViewModels.AccountViewModels;
using TravelBook.Web.ViewModels.TravelViewModels;
using TravelBook.Core.ProjectAggregate;
namespace TravelBook.Web.Service;

public class AppMappingProfile: Profile
{
	public AppMappingProfile()
	{
		CreateMap<IdentityUser, AboutMeViewModel>();
		CreateMap<Travel, TravelsForUserViewModel>();
	}
}
