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
               .AfterMap<SetFeedReadAction>();

            CreateMap<Feed, FeedDto>()
                .ForMember(dto => dto.News, opt => opt.MapFrom(src => src.News))
                .ForMember(dto => dto.UserId, opt => opt.MapFrom((src, dest, destMember, context) => context.Items["UserId"]))
                .ReverseMap();

            CreateMap<CodeHollow.FeedReader.FeedItem, News>();

            CreateMap<CodeHollow.FeedReader.Feed, Feed>()
                 .ForMember(dest => dest.News, opt => opt.MapFrom(x => x.Items));

            CreateMap<RegistrationModel, IdentityUser>();

            CreateMap<IdentityUser, UserDto>();
        }
    }
}
