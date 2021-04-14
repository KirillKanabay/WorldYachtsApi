using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachtsApi.Models;

namespace WorldYachtsApi.Serialization
{
    public class AccessoryMapper:Profile
    {
        public AccessoryMapper()
        {
            //AccessoryModel -> Accessory
            CreateMap<AccessoryModel, Accessory>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dst => dst.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                ;

            //Accessory -> Accessory
            CreateMap<Accessory, Accessory>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dst => dst.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;
        }
    }
}
