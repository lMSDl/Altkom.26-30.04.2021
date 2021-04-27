using Models;
using System;
using System.Collections.Generic;

namespace DataService.Interfaces
{
    public interface IUsersService
    {
        UserRoles? GetRoles(string login, string password);
    }
}
