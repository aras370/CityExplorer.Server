using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.enums;

namespace CityExplorer
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Place, PlaceDTO>()
                .ForMember(dest => dest.PlaceCategory, opt => opt.MapFrom(src => src.PlaceCategory.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.PlaceCategory, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PlaceStatus>(src.Status)));

            CreateMap<PlaceCategory, PlaceCategoryDTO>().ReverseMap();

            CreateMap<CreationPlaceDTO, Place>().ReverseMap();

            CreateMap<PlaceCreationByUserDTO, Place>();

            CreateMap<Place, PlacesListForUsersDTO>()
                .ForMember(dest => dest.PlaceCategory, opt => opt.MapFrom(src => src.PlaceCategory.Name))
                .ReverseMap()
                .ForMember(dest => dest.PlaceCategory, opt => opt.Ignore());


            CreateMap<Place, PlaceDetailDTO>()
              .ForMember(dest => dest.PlaceCategory, opt => opt.MapFrom(src => src.PlaceCategory.Name))
              .ReverseMap()
              .ForMember(dest => dest.PlaceCategory, opt => opt.Ignore());

        }


    }
}
