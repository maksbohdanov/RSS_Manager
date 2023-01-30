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

            CreateMap<CodeHollow.FeedReader.Feed, Feed>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom((src, dest, destMember, context) => context.Items["UserId"])); ;


            CreateMap<RegistrationModel, IdentityUser>();

            CreateMap<IdentityUser, UserDto>();
        }
    }
}
