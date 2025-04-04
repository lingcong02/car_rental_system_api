using AutoMapper;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;

namespace car_rental_system_api.Mapper
{
    public class ImageMapper : Profile
    {
        public ImageMapper()
        {
            CreateMap<Image, ImageViewModel>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));

            CreateMap<ImageViewModel, Image>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));

            CreateMap<ImageRequestViewModel, Image>()
                .ForMember(dest => dest.FkVehicleId, opt => opt.MapFrom(src => src.vehicleId))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}
