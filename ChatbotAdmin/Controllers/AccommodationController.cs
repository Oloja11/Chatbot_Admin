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
    public class AccommodationController : Controller
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILogger<AccommodationController> _logger;
        private readonly List<string> accommodationType = new() { "off-campus", "on-campus" };

        public AccommodationController(IAccommodationRepository accommodationRepository, ILogger<AccommodationController> logger)
        {
            _accommodationRepository = accommodationRepository;
            _logger = logger;
        }
        // GET: AccommodationController
        public ActionResult Index()
        {
            try
            {
                var accommodations = _accommodationRepository.GetAccommodations();
                return View(accommodations.OrderByDescending(v=>v.Id).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching accommodations", ex);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: AccommodationController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var accommodation = _accommodationRepository.GetAccommodations(id);
                if (accommodation != null)
                {
                    return View(accommodation);
                }
                _logger.LogInformation("accommodation not found using id {}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching accommodations with id", ex);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AccommodationController/Create
        public ActionResult Create()
        {
            ViewBag.AccommodationType = accommodationType;
            return View();
        }

        // POST: AccommodationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Accommodation accommodation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newAccommodation = _accommodationRepository.SaveAccommodation(accommodation);
                    if (newAccommodation.Id> 0)
                    {
                        return RedirectToAction(nameof(Details), newAccommodation.Id);                        
                    }
                    ModelState.AddModelError("", "unable to save accommodation now, try again later.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("unable to create accommodation", ex);
                ModelState.AddModelError("", "error occured: " + ex.Message);
            }
            ViewBag.AccommodationType = accommodationType;
            return View(accommodation);
        }

        // GET: AccommodationController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                _logger.LogInformation("about to update accommodation");
                var accommodation = _accommodationRepository.GetAccommodations(id);
                if (accommodation != null)
                {
                    ViewBag.AccommodationType = accommodationType;
                    return View(accommodation);
                }
                _logger.LogInformation("accommodation not found using id {}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching accommodations with id", ex);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: AccommodationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Accommodation accommodation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = _accommodationRepository.UpdateAccommodation(accommodation);
                    if (updated)
                    {
                        return RedirectToAction(nameof(Details), accommodation.Id);                       
                    }
                    ModelState.AddModelError("", "unable to update accommodation now, try again later.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("unable to updae accommodation", ex);
                ModelState.AddModelError("", "error occured: " + ex.Message);
            }
            ViewBag.AccommodationType = accommodationType;
            return View(accommodation);
        }
        
    }
}
