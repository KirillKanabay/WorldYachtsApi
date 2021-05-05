using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;

namespace WorldYachtsApi.Serialization
{
    public class InvoiceMapper:Profile
    {
        public InvoiceMapper()
        {
            //Invoice -> Invoice
            CreateMap<Invoice, Invoice>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dst => dst.ContractId, opt => opt.MapFrom(src => src.ContractId))
                .ForMember(dst => dst.Settled, opt => opt.MapFrom(src => src.Settled))
                .ForMember(dst => dst.Sum, opt => opt.MapFrom(src => src.Sum))
                .ForMember(dst => dst.SumInclVat, opt => opt.MapFrom(src => src.SumInclVat))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Contract, opt => opt.Ignore())
                ;
        }
    }
}
