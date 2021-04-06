using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachtsApi.Entities;
using WorldYachtsApi.Models;
using WorldYachtsApi.Models.Authenticate;

namespace WorldYachtsApi.Serialization
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            CreateMap<User, AuthenticateResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}
