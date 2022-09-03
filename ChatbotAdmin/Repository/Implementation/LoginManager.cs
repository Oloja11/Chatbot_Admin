using ChatbotAdmin.Models;
using ChatbotAdmin.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository.Implementation
{
    public class LoginManager : ILoginManager
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginManager> logger;

        public LoginManager(IConfiguration configuration, ILogger<LoginManager> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public ClubUser Authenticate(string username, string password)
        {
            string sql = "get_club_user_with_login_details";
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    logger.LogInformation("about to log user in {}:{}", username, password);
                    conn.Open();
                    var user = conn.Query<ClubUser>(sql, new { Email = username, HashPassword = password }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                    if (user != null)
                    {
                        user.Role = conn.Query<string>("get_user_roles", new { UserId = user.Id }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                        logger.LogInformation("user authentication successful");
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error authenticating user", ex);
            }
            return null;
        }

        public List<string> GetUserRoles(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
