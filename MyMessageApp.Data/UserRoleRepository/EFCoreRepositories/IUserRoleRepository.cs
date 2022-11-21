﻿using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;

namespace MyMessageApp.Data.UserRoleRepository.EFCoreRepositories
{
    public interface IUserRoleRepository : IRepositoryBase<UserRole>
    {
    }
}