using DataService.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class UsersService : Service<User>, IUsersService
    {
        public UsersService()
        {
            Entities.Add(new User() { Id = 1, Login = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), Roles = (UserRoles)Enum.GetValues<UserRoles>().Cast<int>().Sum() });
            Entities.Add(new User() { Id = 2, Login = "User1", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), Roles = UserRoles.Read | UserRoles.Educator });
            Entities.Add(new User() { Id = 3, Login = "User2", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), Roles = UserRoles.Read | UserRoles.Create });
        }

        public UserRoles? GetRoles(string login, string password)
        {
            var user = Entities.SingleOrDefault(x => x.Login == login);
            if (user == null)
                return null;
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user.Roles;
            return 0;
        }
    }
}
