using System;
using System.Collections.Generic;
using Yms.Contracts.CommonServices;

namespace Yms.Services.Common.Abstractions
{
    public interface IUserService
    {
        UserInfoDto Authenticate(string username, string password);

        IEnumerable<UserDto> GetUsers();

        (bool isSuccess, Guid userId, string code) Register(NewUserDto newUser);
        bool CheckIfCodeAndUserExists(string code, Guid userId);
        void SetPassword(string code, string password);
    }
}
