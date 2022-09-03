using ChatbotAdmin.Repository;
using ChatbotAdmin.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository.Implementation
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UserManagementRepository> logger;

        public UserManagementRepository(IConfiguration configuration, ILogger<UserManagementRepository> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public bool AssignUserToRole(int userId, int roleId)
        {
            try
            {
                logger.LogInformation("about to create assign user {} to role {}", userId,roleId);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var id = conn.ExecuteScalar<int>("assign_user_to_role", new
                    {
                        UserId=userId,
                        RoleId=roleId
                    }, commandType: CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        logger.LogInformation("user assigned to role. Record id: {}", id);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating new user", ex);
            }
            return false;
        }

        public ClubUser CreateUser(ClubUser user)
        {
            try
            {
                logger.LogInformation("about to create user {}", user);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var id = conn.ExecuteScalar<int>("create_club_user", new
                    {
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.PhoneNumber,
                        user.Sex,
                        user.DateOfBirth,
                        user.IsActive,
                        user.HashPassword
                    }, commandType: CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        logger.LogInformation("user created with id: {}", id);
                        user.Id = id;
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating new user", ex);
            }
            return null;
        }        

        public ClubUser GetUser(int id)
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var user = conn.Query<ClubUser>("get_club_user_with_id", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating new user", ex);
            }
            return null;
        }
        public ClubUser GetUserByEmail(string email)
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var user = conn.Query<ClubUser>("get_club_user_by_email", new { email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error fetching user by email", ex);
            }
            return null;
        }

        public List<ClubUser> GetUsers()
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var users = conn.Query<ClubUser>("get_platform_Users", commandType: CommandType.StoredProcedure).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting users", ex);
            }
            return new List<ClubUser>();
        }

        public bool UpdateUserPassword(int userId, string newPassword)
        {
            try
            {
                logger.LogInformation("about to update user password");
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var row = conn.Execute("update_user_password", new
                    {
                        userId,
                        newPassword
                    }, commandType: CommandType.StoredProcedure);
                    if (row > 0)
                    {
                        logger.LogInformation("user password updated", row);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating user password", ex);
            }
            return false;
        }
    }
}
