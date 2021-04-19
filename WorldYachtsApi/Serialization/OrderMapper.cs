using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachtsApi.Models;

namespace WorldYachtsApi.Serialization
{
    public class OrderMapper:Profile
    {
        public OrderMapper()
        {
            //OrderModel -> Order
            CreateMap<OrderModel, Order>()
                .ForMember(dst => dst.BoatId, opt => opt.MapFrom(src => src.BoatId))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dst => dst.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
                .ForMember(dst => dst.SalesPersonId, opt => opt.MapFrom(src => src.SalesPersonId))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                ;

            //Order -> Order
            CreateMap<Order, Order>()
                .ForMember(dst => dst.BoatId, opt => opt.MapFrom(src => src.BoatId))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dst => dst.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
                .ForMember(dst => dst.SalesPersonId, opt => opt.MapFrom(src => src.SalesPersonId))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                ;

            //OrderDetailModel -> OrderDetail
            CreateMap<OrderDetailModel, OrderDetail>()
                .ForMember(dst => dst.AccessoryId, opt => opt.MapFrom(src => src.AccessoryId))
                .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderId))
                ;

            //OrderDetail -> OrderDetail
            CreateMap<OrderDetail, OrderDetail>()
                .ForMember(dst => dst.AccessoryId, opt => opt.MapFrom(src => src.AccessoryId))
                .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderId))
                ;
        }
    }
}
