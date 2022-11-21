using AutoMapper;
using MyMessageApp.Data.AuthenticationRepository.Dtos;
using MyMessageApp.Data.Domain.Entities;

namespace MyMessageApp.Service.Mappers
{
    public class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<User, ApiAuthenticationLoginRequest>().ReverseMap();
        }
    }
}
