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

namespace ChatbotAdmin.Services
{
    public class UtilService
    {
        private readonly IConfiguration _config;

        public List<ApplicationRoles> Roles { get; set; }

        public UtilService(IConfiguration config)
        {
            _config = config;
            getApplicationRoles();
           
        }

        private void getApplicationRoles()
        {
            try
            {
                //_logger.LogInformation("about to get application role");
                using (var conn = new SqlConnection(_config.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var roles = conn.Query<ApplicationRoles>("get_roles", new
                    {
                    }, commandType: CommandType.StoredProcedure).ToList();
                    Roles = roles;
                   // _logger.LogInformation("gotten {} roles", roles.Count);
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError("Error getting roles {}", ex);
            }
        }
    }
}
