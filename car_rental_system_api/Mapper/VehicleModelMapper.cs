using AutoMapper;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;

namespace car_rental_system_api.Mapper
{
    public class VehicleModelMapper : Profile
    {
        public VehicleModelMapper() {
            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VehicleModelId))
                .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Desc));

            CreateMap<VehicleModelViewModel, VehicleModel>()
                .ForMember(dest => dest.VehicleModelId, opt => opt.Ignore())
                .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Desc))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}
