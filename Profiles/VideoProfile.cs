using AluraFlix.Data.Dtos.VideoDto;
using AluraFlix.Models;
using AutoMapper;

namespace AluraFlix.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<CreateVideoDto, Video>();
            CreateMap<Video, ReadVideoDto>();
            CreateMap<UpdateVideoDto, Video>();
        }
    }
}
