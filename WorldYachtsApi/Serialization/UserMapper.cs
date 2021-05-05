using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachtsApi.Models;


namespace WorldYachtsApi.Serialization
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            //CustomerModel -> Customer
            CreateMap<CustomerModel, Customer>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src=>src.Address))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src=>src.City))
                .ForMember(dst => dst.Phone, opt => opt.MapFrom(src=>src.Phone))
                .ForMember(dst => dst.OrganizationName, opt => opt.MapFrom(src=>src.OrganizationName))
                .ForMember(dst => dst.IdNumber, opt => opt.MapFrom(src=>src.IdNumber))
                .ForMember(dst => dst.IdDocumentName, opt => opt.MapFrom(src=>src.IdDocumentName))
                ;

            //CustomerModel -> User
            CreateMap<CustomerModel, User>()
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            //Customer -> Customer
            CreateMap<Customer, Customer>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dst => dst.OrganizationName, opt => opt.MapFrom(src => src.OrganizationName))
                .ForMember(dst => dst.IdNumber, opt => opt.MapFrom(src => src.IdNumber))
                .ForMember(dst => dst.IdDocumentName, opt => opt.MapFrom(src => src.IdDocumentName))
                ;

            //SalesPerson -> SalesPerson
            CreateMap<SalesPerson, SalesPerson>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.SecondName, opt => opt.MapFrom(src => src.SecondName))
                ;

            //SalesPersonModel -> SalesPerson
            CreateMap<SalesPersonModel, SalesPerson>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.SecondName, opt => opt.MapFrom(src => src.SecondName))
                ;

            CreateMap<User, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Username))
                ;

            //SalesPersonModel -> User
            CreateMap<SalesPersonModel, User>()
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Id, opt=> opt.Ignore())
                ;
        }
    }
}
