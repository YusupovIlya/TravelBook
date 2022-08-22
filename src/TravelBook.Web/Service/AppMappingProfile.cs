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
		CreateMap<NewTravelViewModel, Travel>()
			.ForMember(dest => dest.Place, opt => opt.MapFrom(src => new Place(src.City, src.Country)));

		CreateMap<Travel, TravelDeleteViewModel>()
			.ForMember(dest => dest.NumAlbums, opt => opt.MapFrom(src => src.PhotoAlbums.Count()))
			.ForMember(dest => dest.NumArticles, opt => opt.MapFrom(src => src.Articles.Count()))
			.ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place.ToString()));

		CreateMap<Article, ArticleViewModel>()
			.ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Travel.Place.ToString()));

		CreateMap<NewArticleViewModel, Article>();

		CreateMap<Article, EditArticleViewModel>().ReverseMap();

		CreateMap<PhotoAlbum, PhotoAlbumViewModel>()
			.ForMember(dest => dest.Travel, opt => opt.MapFrom(src => src.Travel.Place.ToString()))
			.ForMember(dest => dest.NumPhotos, opt => opt.MapFrom(src => src.Photos.Count()));

		CreateMap<PhotoAlbum, OpenPhotoAlbumViewModel>()
			.ForMember(dest => dest.Travel, opt => opt.MapFrom(src => src.Travel.Place.ToString()));

		CreateMap<Photo, EditPhotoViewModel>().ReverseMap();
	}
}
