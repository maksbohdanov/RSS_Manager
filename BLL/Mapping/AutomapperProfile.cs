using AutoMapper;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Mapping
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<News, NewsDto>()
               .ReverseMap();

            CreateMap<Feed, FeedDto>()
                //.ForMember(dto => dto.News, x => x.MapFrom(f => f.News.Select(n => n)))
                .ReverseMap();

            CreateMap<RegistrationModel, IdentityUser>();

            CreateMap<IdentityUser, UserDto>();
        }
    }
}
