﻿using CRM.Application.Abstraction;

namespace CRM.Application.Abstraction
{
    public interface IUserService
    {
        Task<List<AppUserVM>> GetUsers();

    }
}
