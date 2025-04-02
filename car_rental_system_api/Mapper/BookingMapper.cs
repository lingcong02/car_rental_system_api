using AutoMapper;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;

namespace car_rental_system_api.Mapper
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<Booking, BookingViewModel>()
                .ForMember(dest => dest.BookingNo, opt => opt.MapFrom(src => src.BookingNo))
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.FkVehicleId))
                .ForMember(dest => dest.CustName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<BookingViewModel, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingNo, opt => opt.MapFrom(src => src.BookingNo))
                .ForMember(dest => dest.FkVehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.FkUserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustEmail))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustPhone))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.Now));


        }
    }
}
