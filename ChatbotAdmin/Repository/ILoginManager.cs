using ChatbotAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository
{
    public interface ILoginManager
    {
        ClubUser Authenticate(string username, string password);
        List<string> GetUserRoles(int userId);
    }
}
