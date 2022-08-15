using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TravelBook.Web.ViewModels.AccountViewModels;
using TravelBook.Web.ViewModels.TravelViewModels;
using TravelBook.Web.ViewModels.ArticleViewModels;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Core.ProjectAggregate;
namespace TravelBook.Web.Service;

public class AppMappingProfile: Profile
{
	public AppMappingProfile()
	{
		CreateMap<IdentityUser, AboutMeViewModel>();
		CreateMap<Travel, TravelViewModel>();

		CreateMap<Article, ArticleViewModel>()
			.ForMember(dest => dest.Place, opt => opt.MapFrom(src => $"{src.Travel.Place.Country}, {src.Travel.Place.City}"));

		CreateMap<PhotoAlbum, PhotoAlbumViewModel>()
			.ForMember(dest => dest.Travel, opt => opt.MapFrom(src => $"{src.Travel.Place.Country}, {src.Travel.Place.City}"))
			.ForMember(dest => dest.NumPhotos, opt => opt.MapFrom(src => src.Photos.Count()));

		CreateMap<PhotoAlbum, OpenPhotoAlbumViewModel>()
			.ForMember(dest => dest.Travel, opt => opt.MapFrom(src => $"{src.Travel.Place.Country}, {src.Travel.Place.City}"));
	}
}
