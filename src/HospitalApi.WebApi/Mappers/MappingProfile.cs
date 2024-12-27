using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.MedicalServices;
using HospitalApi.WebApi.Models.News;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Recipes;
using HospitalApi.WebApi.Models.Tickets;
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
                CheckUps = context.Mapper.Map<IEnumerable<LaboratoryViewModel>>(src.CheckUps),
                PdfDetails = context.Mapper.Map<PdfDetailsViewModel>(src.PdfDetails)
            })
            .ReverseMap();
        CreateMap<RecipeCreateModel, Recipe>()
            .ForMember(dest => dest.CheckUps, opt => opt.Ignore());
        CreateMap<RecipeUpdateModel, Recipe>()
            .ForMember(dest => dest.CheckUps, opt => opt.Ignore());

        CreateMap<NewsList, NewsListViewModel>().ReverseMap();
        CreateMap<NewsListCreateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();
        CreateMap<NewsListUpdateModel, NewsList>().ForMember(dest => dest.Picture, opt => opt.Ignore()).ReverseMap();

        CreateMap<Laboratory, LaboratoryViewModel>()
            .ConstructUsing((src, context) => new LaboratoryViewModel
            {
                PdfDetails = context.Mapper.Map<PdfDetailsViewModel>(src.PdfDetails),
            })
            .ReverseMap();
        CreateMap<ClinicQueue, ClinicQueueViewModel>().ReverseMap();
        CreateMap<MedicalServiceType, MedicalServiceTypeViewModel>()
            .ConstructUsing((src, context) =>
            {
                return new MedicalServiceTypeViewModel
                {
                    Staff = context.Mapper.Map<UserViewModel>(src.Staff),
                    ClinicQueue = context.Mapper.Map<ClinicQueueViewModel>(src.ClinicQueue),
                };
            });
        CreateMap<MedicalServiceTypeCreateModel, MedicalServiceType>().ReverseMap();
        CreateMap<MedicalServiceTypeUpdateModel, MedicalServiceType>().ReverseMap();

        CreateMap<MedicalServiceTypeHistory, MedicalServiceTypeHistoryViewModel>()
            .ConstructUsing((src, context) => new MedicalServiceTypeHistoryViewModel
            {
                Client = context.Mapper.Map<UserViewModel>(src.Client),
                MedicalServiceType = context.Mapper.Map<MedicalServiceTypeViewModel>(src.MedicalServiceType)
            });

        CreateMap<Ticket, TicketViewModelModel>()
            .ConstructUsing((src, context) => new TicketViewModelModel
            {
                Client = context.Mapper.Map<UserViewModel>(src.Client),
                MedicalServiceTypeHistories = context.Mapper.Map<IEnumerable<MedicalServiceTypeHistoryViewModel>>(src.MedicalServiceTypeHistories),
                PdfDetails = context.Mapper.Map<PdfDetailsViewModel>(src.PdfDetails)
            });
        CreateMap<TicketCreateModel, TicketCreateDto>().ReverseMap();
        CreateMap<PdfDetails, PdfDetailsViewModel>().ReverseMap();

        CreateMap<MedicalServiceTypeStatisticsDetailsDto, Models.Statistics.MedicalServiceTypeStatisticsDetailsDto>()
            .ConstructUsing((src, context) => new Models.Statistics.MedicalServiceTypeStatisticsDetailsDto
            {
                MedicalServiceType = context.Mapper.Map<MedicalServiceTypeViewModel>(src.MedicalServiceType)
            });
        CreateMap<StatisticsDetailsDto, Models.Statistics.StatisticsDetailsDto>()
            .ConstructUsing((src, context) => new Models.Statistics.StatisticsDetailsDto
            {
                MedicalServiceTypeStatistics = context.Mapper.Map<IEnumerable<Models.Statistics.MedicalServiceTypeStatisticsDetailsDto>>(src.MedicalServiceTypeStatistics)
            });
    }
}