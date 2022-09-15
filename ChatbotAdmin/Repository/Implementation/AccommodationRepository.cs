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
    public class AccommodationRepository : IAccommodationRepository
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<LoginManager> logger;

        public AccommodationRepository(IConfiguration configuration, ILogger<LoginManager> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public Accommodation GetAccommodations(long id)
        {
            try
            {
                logger.LogInformation("about to get accommodation by id {}", id);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var accommodation = conn.Query<Accommodation>("get_accommodation_by_id", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    logger.LogInformation("finished getting accommodation with id {}", id);
                    return accommodation;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting accommodation by id", ex);
            }
            return null;
        }

        public List<Accommodation> GetAccommodations()
        {
            try
            {
                logger.LogInformation("about to get all accommodation");
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var accommodations = conn.Query<Accommodation>("get_accommodations", new { }, commandType: CommandType.StoredProcedure).ToList();
                    logger.LogInformation("getting courses completed");
                    return accommodations;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting accommodation", ex);
            }
            return new List<Accommodation>();
        }

        public Accommodation SaveAccommodation(Accommodation accommodation)
        {
            try
            {
                logger.LogInformation("about to save Accommodation {}", accommodation);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var id = conn.ExecuteScalar<int>("create_accommodation",
                        new
                        {
                            type = accommodation.AccommodationType,
                            accommodation.Name,
                            accommodation.Description,
                            accommodation.Amount,
                            accommodation.Link
                        },
                        commandType: CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        logger.LogInformation("Accommodation created ID: {}", id);
                        accommodation.Id = id;
                        return accommodation;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating Accommodation", ex);
            }
            return null;
        }

        public bool UpdateAccommodation(Accommodation accommodation)
        {
            try
            {
                logger.LogInformation("about to update accommodation {}", accommodation);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var rowAffected = conn.Execute("update_accommodation",
                        new
                        {
                            accommodation.Id,
                            type = accommodation.AccommodationType,
                            accommodation.Name,
                            accommodation.Description,
                            accommodation.Amount,
                            accommodation.Link
                        },
                        commandType: CommandType.StoredProcedure);
                    if (rowAffected > 0)
                    {
                        logger.LogInformation("accommodation updated");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating accommodation", ex);
            }
            return false;
        }
    }
}
