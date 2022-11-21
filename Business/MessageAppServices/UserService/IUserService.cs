using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MessageAppServices.UserService
{
    public interface IUserService
    {
        Task<List<UserListDto>> Getall();
        Task Create(UserCreateDto userCreateDto);
        Task<UserListDto> GetById(object id);
        Task Remove(object id);
        Task Update(UserUpdateDto dto);
    }
}
