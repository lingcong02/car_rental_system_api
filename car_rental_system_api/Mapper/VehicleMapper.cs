using AutoMapper;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;

namespace car_rental_system_api.Mapper
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.FkVehicleModelId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PlatNo, opt => opt.MapFrom(src => src.PlatNo))
                .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Desc))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images));

            

            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.VehicleId, opt => opt.Ignore())
                .ForMember(dest => dest.FkVehicleModelId, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.PlatNo, opt => opt.MapFrom(src => src.PlatNo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Desc))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}
