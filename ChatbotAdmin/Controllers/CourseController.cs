using ChatbotAdmin.Models;
using ChatbotAdmin.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<AccommodationController> _logger;
        private readonly List<string> courseLevel = new() { "postgraduate", "undergraduate" };

        public CourseController(ICourseRepository courseRepository, ILogger<AccommodationController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }
        // GET: CourseController
        public ActionResult Index()
        {
            try
            {
                var courses = _courseRepository.GetCourses();
                return View(courses.OrderByDescending(v=>v.Id).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching courses", ex);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var course = _courseRepository.GetCourses(id);
                if (course != null)
                {
                    return View(course);
                }
                _logger.LogInformation("course not found using id {}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching course with id", ex);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            ViewBag.Level = courseLevel;
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCourse = _courseRepository.SaveCourse(course);
                    if (newCourse.Id > 0)
                    {
                        return RedirectToAction(nameof(Details), newCourse.Id);
                    }
                    ModelState.AddModelError("", "unable to save course now, try again later.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("unable to create course", ex);
                ModelState.AddModelError("", "error occured: " + ex.Message);
            }
            ViewBag.Level = courseLevel;
            return View(course);
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                _logger.LogInformation("about to update course");
                var course = _courseRepository.GetCourses(id);
                if (course != null)
                {
                    ViewBag.Level = courseLevel;
                    return View(course);
                }
                _logger.LogInformation("course not found using id {}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching course with id", ex);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = _courseRepository.UpdateCourse(course);
                    if (updated)
                    {
                        return RedirectToAction(nameof(Details), course.Id);
                    }
                    ModelState.AddModelError("", "unable to update curse now, try again later.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("unable to updae course", ex);
                ModelState.AddModelError("", "error occured: " + ex.Message);
            }
            ViewBag.Level = courseLevel;
            return View(course);
        }        
    }
}
