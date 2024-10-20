﻿using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.News;
using HospitalApi.WebApi.Models.Recipes;
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

        CreateMap<Booking, BookingViewModel>().ReverseMap();
        CreateMap<BookingCreateModel, Booking>().ReverseMap();
        CreateMap<BookingUpdateModel, Booking>().ReverseMap();

        CreateMap<Recipe, RecipeViewModel>().ReverseMap();
        CreateMap<RecipeCreateModel, Recipe>().ReverseMap();
        CreateMap<RecipeUpdateModel, Recipe>().ReverseMap();

        CreateMap<NewsList, NewsListViewModel>().ReverseMap();
        CreateMap<NewsListCreateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();
        CreateMap<NewsListUpdateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();
    }
}

