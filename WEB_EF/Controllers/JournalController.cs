using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebApi.Models.Entities;
using WebApi.Models.DBContexts;
using WebApi.Models.Interfaces;

namespace WebApi.Controllers
{
    public class JournalController : Controller
    {
        private readonly IAutoparkDBContext _context;
        private readonly ICRUDlService<Journal> _service;
        private readonly IValidateService<Journal> _validateService;

        public JournalController(IAutoparkDBContext context, ICRUDlService<Journal> service, IValidateService<Journal> validateService)
        {
            _context = context;
            _service = service;
            _validateService = validateService;
        }


        // GET: JournalController
        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Include(j=>j.Car).Include(j=>j.ParkingPlaceNavigation).ToList());
        }

        // GET: JournalController/Create
        public ActionResult Create()
        {
            ViewData["Cars"] = _context.Cars.ToList();
            ViewData["ParkingPlaces"] = _context.ParkingPlaces.ToList();
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            return View();
        }

        // POST: JournalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int carID, int parkingPlace, DateTime comingDate, DateTime? departureDate, IFormCollection collection)
        {
            try
            { 
                Journal journalRecord = new()
                {
                    CarId = carID,
                    ComingDate = comingDate,
                    ParkingPlace = parkingPlace,
                    DepartureDate = departureDate
                };

                if (!_validateService.Validate(journalRecord, out string exp))
                {
                    ViewData["Message"] = exp;
                    return Create();
                }

                _service.Create(journalRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Create();
            }
        }

        // GET: JournalController/Edit/5
        public ActionResult Edit(int id)
        {
            var record = _service.GetViaIQueriable().FirstOrDefault(j => j.Id == id);
            if (record == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["Cars"] = _context.Cars.ToList();
            ViewData["ParkingPlaces"] = _context.ParkingPlaces.ToList();
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            return View(record);
        }
        // POST: JournalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int carID, int parkingPlace, DateTime comingDate, DateTime? departureDate, IFormCollection collection)
        {
            try
            {
                var journalRecord = _service.GetViaIQueriable().FirstOrDefault(j => j.Id == id);
                if (journalRecord == null)
                {
                    ViewData["Message"] = "Record not found";
                    return Edit(id);
                }

                if (!_validateService.Validate(journalRecord, out string exp))
                {
                    ViewData["Message"] = exp; 
                    return Edit(id);
                }

                _service.Update(journalRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Edit(id);
            }
        }

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Journal? journalRecord = _service.GetViaIQueriable().FirstOrDefault(j => j.Id == id);
                if (journalRecord == null)
                {
                    ViewData["Message"] = "Record not found";
                    return Edit(id);
                }

                _service.Delete(journalRecord);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
