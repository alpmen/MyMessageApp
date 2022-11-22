using AutoMapper;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;

namespace MyMessageApp.Service.Mappers
{
    public class UserProfle:Profile
    {
        public UserProfle()
        {
            CreateMap<User, UserCreateRequest>().ReverseMap();
            CreateMap<User, UserUpdateRequest>().ReverseMap();
            CreateMap<User, UsersListAllResult>().ReverseMap();
            CreateMap<User, UserGetByIdResponse>().ReverseMap();
        }
    }
}