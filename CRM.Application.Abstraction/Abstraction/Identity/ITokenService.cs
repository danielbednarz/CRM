﻿namespace CRM.Application.Abstraction
{
    public interface ITokenService
    {
        public string CreateToken(string username);
    }
}
