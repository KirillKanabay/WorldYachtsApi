using AutoMapper;
using WorldYachts.Data.Entities;

namespace WorldYachtsApi.Serialization
{
    public class ContractMapper:Profile
    {
        public ContractMapper()
        {
            //Contract -> Contract
            CreateMap<Contract, Contract>()
                .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dst => dst.ContractTotalPrice, opt => opt.MapFrom(src => src.ContractTotalPrice))
                .ForMember(dst => dst.ContractTotalPriceInclVat, opt => opt.MapFrom(src => src.ContractTotalPriceInclVat))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dst => dst.DepositPayed, opt => opt.MapFrom(src => src.DepositPayed))
                .ForMember(dst => dst.ProductionProcess, opt => opt.MapFrom(src => src.ProductionProcess))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Order, opt => opt.Ignore())
                ;
        }
    }
}
