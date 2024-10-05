using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;

namespace ApiDevBP.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}
