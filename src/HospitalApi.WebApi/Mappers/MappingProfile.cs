using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.News;
using HospitalApi.WebApi.Models.Recipes;
using HospitalApi.WebApi.Models.Tables;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetViewModel>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<StaffCreateModel, User>().ReverseMap();
        CreateMap<StaffUpdateModel, User>().ReverseMap();

        CreateMap<Booking, BookingViewModel>().ReverseMap();
        CreateMap<BookingCreateModel, Booking>().ReverseMap();
        CreateMap<BookingUpdateModel, Booking>().ReverseMap();
        CreateMap<(User, IEnumerable<TimesOfBooking>), BookingViewModelByDate>()
            .ConstructUsing((src, context) => new BookingViewModelByDate
            {
                UserViewModel = context.Mapper.Map<UserViewModel>(src.Item1),
                BookedTimes = src.Item2
            })
            .ReverseMap();

        CreateMap<Recipe, RecipeViewModel>()
            .ConstructUsing((src, context) => new RecipeViewModel
            {
                Client = context.Mapper.Map<UserViewModel>(src.Client),
                Staff = context.Mapper.Map<UserViewModel>(src.Staff),
                Picture = context.Mapper.Map<AssetViewModel>(src.Picture),
                DateOfVisit = src.CreatedAt
            })
            .ReverseMap();
        CreateMap<RecipeCreateModel, Recipe>()
            .ForMember(member => member.Picture, option => option.Ignore())
            .ReverseMap();
        CreateMap<RecipeUpdateModel, Recipe>()
            .ForMember(member => member.Picture, option => option.Ignore())
            .ReverseMap();

        CreateMap<NewsList, NewsListViewModel>().ReverseMap();
        CreateMap<NewsListCreateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();
        CreateMap<NewsListUpdateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();

        CreateMap<Laboratory, LaboratoryViewModel>().ReverseMap();
    }
}