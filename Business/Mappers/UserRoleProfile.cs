using AutoMapper;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleCreateDto>().ReverseMap();
            CreateMap<UserRole, UserRoleUpdateDto>().ReverseMap();
            CreateMap<UserRole, UserRoleListDto>().ReverseMap();
        }
    }
}
