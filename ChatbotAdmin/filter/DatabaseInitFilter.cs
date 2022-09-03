using ChatbotAdmin.Models;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatbotAdmin.filter
{
    public class DatabaseInitFilter : IStartupFilter
    {
        private readonly IConfiguration configuration;
        //private readonly DbLogger<DatabaseInitFilter> logger;

        private readonly ILogger<DatabaseInitFilter> _logger;

        public DatabaseInitFilter(IConfiguration configuration, ILogger<DatabaseInitFilter> logger)
        {
            this.configuration = configuration;
            //this.logger = logger;
            _logger = logger;
        }
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            string connectionString = configuration.GetConnectionString("ChatAdminDatabase");
            var dbUpgradeEngine = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                _logger.LogInformation("New database upgrade available. Upgrading now ....");
                var response = dbUpgradeEngine.PerformUpgrade();
                if (response.Successful)
                {
                    _logger.LogInformation("database upgrade successful.");
                }
                else
                {
                    _logger.LogInformation("Error occured during database upgrade. Please check log for informaion.");
                }
            }
            return next;
        }
    }
}
