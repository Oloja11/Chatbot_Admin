using ChatbotAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository
{
    public interface IUserManagementRepository
    {
        ClubUser CreateUser(ClubUser user);
        ClubUser GetUser(int id);
        List<ClubUser> GetUsers();
        bool AssignUserToRole(int userId, int roleId);
        ClubUser GetUserByEmail(string email);
        bool UpdateUserPassword(int userId, string newPassword);


    }
}
