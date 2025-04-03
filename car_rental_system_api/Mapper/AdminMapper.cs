using AutoMapper;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.Helper;
using car_rental_system_api.ViewModel;

namespace car_rental_system_api.Mapper
{
    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<Admin, AdminViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(_ => string.Empty));

            CreateMap<AdminViewModel, Admin>()
                .ForMember(dest => dest.AdminId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.Now))
                .AfterMap((src, dest) =>
                {
                    var encrypted = CryptoHelper.HashPassword(src.Password);
                    dest.Hash = encrypted.hash;
                    dest.Guid = encrypted.guid;
                });
        }
    }
}
