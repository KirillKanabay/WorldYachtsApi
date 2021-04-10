using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Models;

namespace WorldYachtsApi.Serialization
{
    public class BoatMapper:Profile
    {
        public BoatMapper()
        {
            //BoatModel -> Boat
            CreateMap<BoatModel, Boat>()
                .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dst => dst.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dst => dst.WoodId, opt => opt.MapFrom(src => src.WoodId))
                .ForMember(dst => dst.NumberOfRowers, opt => opt.MapFrom(src => src.NumberOfRowers))
                .ForMember(dst => dst.Mast, opt => opt.MapFrom(src => src.Mast))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                ;

            //BoatTypeModel -> BoatType
            CreateMap<BoatTypeModel, BoatType>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                ;
        }
    }
}
