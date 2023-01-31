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
                .ForMember(dto => dto.News, x => x.MapFrom(f => f.News))
                .ForMember(dto => dto.UserId, x => x.MapFrom((src, dest, destMember, context) => context.Items["UserId"]))
                .ReverseMap();

            CreateMap<CodeHollow.FeedReader.FeedItem, News>();

            CreateMap<CodeHollow.FeedReader.Feed, Feed>()
                 .ForMember(dest => dest.News, opt => opt.MapFrom(x => x.Items));

            CreateMap<RegistrationModel, IdentityUser>();

            CreateMap<IdentityUser, UserDto>();
        }
    }
}
