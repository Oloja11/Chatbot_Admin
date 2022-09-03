using ChatbotAdmin.Models;
using ChatbotAdmin.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotAdmin.Services
{
    public class UserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly ILogger<UserManagementService> _logger;
        private readonly EmailSenderService _emailSenderService;
        private readonly UtilService _util;

        public UserManagementService(IUserManagementRepository userManagementRepository, ILogger<UserManagementService> logger, UtilService util, EmailSenderService emailSenderService)
        {
            _userManagementRepository = userManagementRepository;
            _logger = logger;
            _util = util;
            _emailSenderService = emailSenderService;
        }

        public ClubUser CreateUser(ClubUser user)
        {
            try
            {
                if (user != null)
                {                    
                    return _userManagementRepository.CreateUser(user);
                }               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in creating user", ex);
            }
            return null;
        }

        public ClubUser GetUser(int id)
        {
            try
            {
                if (id>0)
                {
                    return _userManagementRepository.GetUser(id);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in creating user", ex);
            }
            return null;
        }

        public ClubUser GetUserByEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    return _userManagementRepository.GetUserByEmail(email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user", ex);
            }
            return null;
        }

        public List<ClubUser> GetUsers()
        {
            try
            {

                return _userManagementRepository.GetUsers();
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user", ex);
            }
            return new List<ClubUser>();
        }

        public bool AssignUserToRole(int userId, string roleName)
        {
            try
            {
                var role = _util.Roles.Find(r => r.Name == roleName);
                if (role !=null)
                {
                    return _userManagementRepository.AssignUserToRole(userId,role.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error assigning user to role {}", ex);
            }
            return false;
        }

        public void SendPasswordToUser(string email)
        {
            var user = GetUserByEmail(email);
            if (user!=null)
            {
                SendUserPasswordNotification(user);
                return;
            }
            throw new ArgumentException("Invalid email address");
        }

        public bool ChangeUserPassword(int userId, string currentPassword, string newPassword)
        {
            var user = GetUser(userId);
            if (user!=null)
            {
                var currentPasswordHashed = Convert.ToBase64String(Encoding.UTF8.GetBytes(currentPassword));
                if (!CurrentPasswordEqual(user.HashPassword, currentPasswordHashed))
                {
                    throw new ArgumentException("Current password not valid");
                }

                var newPasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(newPassword));

                return _userManagementRepository.UpdateUserPassword(userId, newPasswordHash);
            }
            return false;
        }

        private static bool CurrentPasswordEqual(string hashPassword, string currentPassword) => hashPassword == currentPassword;
        

        private void SendUserPasswordNotification(ClubUser user)
        {
            try
            {
                var plainPassword = Encoding.UTF8.GetString(Convert.FromBase64String(user.HashPassword));
                var body = $"Dear {user.FirstName} {user.LastName},<br><br> Your log-in details is as follows: <br>" +
                $"Username: {user.Email} <br> Password: {plainPassword} <br><br>" +
                $"Regard";
                var response = _emailSenderService.Send(user.Email, "Login Details", body);

                _logger.LogInformation("Email sending response: {}", response);
            }
            catch (Exception ex)
            {
                _logger.LogError("error sending email {}", ex.Message);
            }
        }
    }
}
