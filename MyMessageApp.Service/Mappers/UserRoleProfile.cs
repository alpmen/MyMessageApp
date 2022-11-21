using AutoMapper;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;

namespace MyMessageApp.Service.Mappers
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleCreateRequest>().ReverseMap();
            CreateMap<UserRole, UserRoleUpdateRequest>().ReverseMap();
            CreateMap<UserRole, UserRoleListAllResult>().ReverseMap();
            CreateMap<UserRole, UserRoleGetByIdResult>().ReverseMap();
            CreateMap<UserRole, UserRoleGetByFilterResult>().ReverseMap();
        }
    }
}