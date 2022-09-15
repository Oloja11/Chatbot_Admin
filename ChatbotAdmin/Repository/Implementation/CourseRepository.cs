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
    public class CourseRepository : ICourseRepository
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<LoginManager> logger;

        public CourseRepository(IConfiguration configuration, ILogger<LoginManager> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        
        public Course GetCourses(long id)
        {
            try
            {
                logger.LogInformation("about to get course by id {}", id);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var message = conn.Query<Course>("get_course_by_id", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    logger.LogInformation("finished getting course with id {}", id);
                    return message;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting course by id", ex);
            }
            return null;
        }

        public List<Course> GetCourses()
        {
            try
            {
                logger.LogInformation("about to get all courses");
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var messages = conn.Query<Course>("get_courses", new { }, commandType: CommandType.StoredProcedure).ToList();
                    logger.LogInformation("getting courses completed");
                    return messages;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting courses", ex);
            }
            return new List<Course>();
        }

        public Course SaveCourse(Course course)
        {
            try
            {
                logger.LogInformation("about to save course {}", course);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var id = conn.ExecuteScalar<int>("create_course",
                        new
                        {
                            course.Department,
                            course.Duration,
                            course.Level,
                            course.Title
                        },
                        commandType: CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        logger.LogInformation("course created ID: {}", id);
                        course.Id = id;
                        return course;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating course", ex);
            }

            return null;
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                logger.LogInformation("about to update course {}", course);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var rowAffected = conn.Execute("update_course",
                        new
                        {
                            course.Id,
                            course.Department,
                            course.Duration,
                            course.Level,
                            course.Title
                        },
                        commandType: CommandType.StoredProcedure);
                    if (rowAffected > 0)
                    {
                        logger.LogInformation("course updated");                        
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating course", ex);
            }
            return false;
        }
    }
}
