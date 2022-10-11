using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class JournalController : Controller
    {
        private readonly AutoparkContext _context;

        public JournalController(AutoparkContext context)
        {
            _context = context;
        }


        // GET: JournalController
        public ActionResult Index()
        {
            return View(_context.Journals.Where(j => !j.IsDeleted).Include(j=>j.Car).Include(j=>j.ParkingPlaceNavigation).ToList());
        }

        // GET: JournalController/Create
        public ActionResult Create()
        {
            ViewData["Cars"] = _context.Cars.Where(c => !c.IsDeleted).ToList();
            ViewData["ParkingPlaces"] = _context.ParkingPlaces.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = _context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View();
        }

        // POST: JournalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int carID, int parkingPlace, DateTime comingDate, DateTime? departureDate, IFormCollection collection)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(c => !c.IsDeleted && c.Id == carID);
                if (car == null)
                {
                    ViewData["Message"] = "Car not found";
                    return Create();
                }

                var parkingPlaceNavigation = _context.ParkingPlaces.FirstOrDefault(c => !c.IsDeleted && c.Id == parkingPlace);
                if (parkingPlaceNavigation == null)
                {
                    ViewData["Message"] = "Parking place not found";
                    return Create();
                }

                Journal journalRecord = new()
                {
                    Car = car,
                    CarId = carID,
                    ComingDate = comingDate,
                    ParkingPlace = parkingPlace,
                    ParkingPlaceNavigation = parkingPlaceNavigation,
                    DepartureDate = departureDate
                };

                if (IsRecordAdequate(journalRecord, out string exp))
                {
                    _context.Journals.Add(journalRecord);
                    _context.SaveChanges();
                }
                else
                {
                    ViewData["Message"] = exp;
                    return Create();
                }

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
            var record = _context.Journals.FirstOrDefault(j => !j.IsDeleted && j.Id == id);
            if (record == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["Cars"] = _context.Cars.Where(c => !c.IsDeleted).ToList();
            ViewData["ParkingPlaces"] = _context.ParkingPlaces.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = _context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View(record);
        }
        // POST: JournalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int carID, int parkingPlace, DateTime comingDate, DateTime? departureDate, IFormCollection collection)
        {
            try
            {
                Journal? journalRecord = _context.Journals.FirstOrDefault(j => !j.IsDeleted && j.Id == id);
                if (journalRecord == null)
                {
                    ViewData["Message"] = "Record not found";
                    return Edit(id);
                }

                var car = _context.Cars.FirstOrDefault(c => !c.IsDeleted && c.Id == carID);
                if (car == null)
                {
                    ViewData["Message"] = "Car not found";
                    return Create();
                }

                var parkingPlaceNavigation = _context.ParkingPlaces.FirstOrDefault(c => !c.IsDeleted && c.Id == parkingPlace);
                if (parkingPlaceNavigation == null)
                {
                    ViewData["Message"] = "Parking place not found";
                    return Create();
                }

                journalRecord.Car = car;
                journalRecord.CarId = carID;
                journalRecord.ComingDate = comingDate;
                journalRecord.ParkingPlace = parkingPlace;
                journalRecord.ParkingPlaceNavigation = parkingPlaceNavigation;
                journalRecord.DepartureDate = departureDate;
                if (IsRecordAdequate(journalRecord, out string exp))
                {
                    _context.SaveChanges();
                }
                else
                {
                    ViewData["Message"] = exp; 
                    return Edit(id);
                }

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
                Journal? journalRecord = _context.Journals.FirstOrDefault(j => !j.IsDeleted && j.Id == id);
                if (journalRecord == null)
                {
                    ViewData["Message"] = "Record not found";
                    return Edit(id);
                }

                _context.Journals.Remove(journalRecord);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private bool IsRecordAdequate(Journal record, out string explanation)
        {
            if (record == null)
            {
                explanation = "Record is null";
                return false;
            }

            if (record.IsDeleted)
            {
                explanation = "Record is deleted";
                return false;
            }

            if (record.Car == null)
            {
                explanation = "Car is null";
                return false;
            }

            if (record.ParkingPlaceNavigation == null)
            {
                explanation = "Parking place is null";
                return false;
            }

            if (record.Car.CarType != record.ParkingPlaceNavigation.CarType)
            {
                explanation = "Car's type is not equal to parking place's type";
                return false;
            }

            var comingDate = record.ComingDate;
            var departureDate = record.DepartureDate ?? DateTime.MaxValue;
            if (comingDate > departureDate)
            {
                explanation = "Coming date is bigger than departure date";
                return false;
            }

            if (_context.Journals.Any(j => !j.IsDeleted && j.Id != record.Id && j.CarId == record.CarId && (j.ComingDate <= departureDate && (j.DepartureDate?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Car is in parking at this period";
                return false;
            }

            if (_context.Journals.Any(j => !j.IsDeleted && j.Id != record.Id && j.ParkingPlace == record.ParkingPlace && (j.ComingDate <= departureDate && (j.DepartureDate ?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Place is taken at this period";
                return false;
            }


            explanation = string.Empty;
            return true;
        }    
    }
}
