using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository
{
    public interface ICourseRepository
    {
        Course GetCourses(long id);
        List<Course> GetCourses();

        Course SaveCourse(Course course);
        bool UpdateCourse(Course course);



    }
}
