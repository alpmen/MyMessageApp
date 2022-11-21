using AutoMapper;
using MyMessageApp.Data.AuthenticationRepository.Dtos;
using MyMessageApp.Data.Domain.Entities;

namespace Business.Mappers
{
    public class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<User, ApiAuthenticationLoginRequest>().ReverseMap();
        }
    }
}
