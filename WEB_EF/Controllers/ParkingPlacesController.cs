using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.DBContexts;
using WEB_EF.Models.Classes;

namespace WEB_EF.Controllers
{
    public class ParkingPlacesController : Controller
    {
        private readonly AutoparkContext _context = new();

        public ParkingPlacesController(AutoparkContext context)
        {
            _context = context;
        }

        // GET: ClientsController
        public ActionResult Index()
        {
            ViewData["CarTypes"] = _context.CarTypes.Where(ct => !ct.IsDeleted).ToList();
            return View(_context.ParkingPlaces.Where(j => !j.IsDeleted).ToList());
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            ViewData["CarTypes"] = _context.CarTypes.Where(ct=>!ct.IsDeleted).ToList();
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int carType, IFormCollection collection)
        {
            try
            {
                var carTypeNavigation = _context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == carType);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                var parkingPlace = new ParkingPlace()
                {
                    CarType = carTypeNavigation.Id,
                    CarTypeNavigation = carTypeNavigation,
                };

                _context.ParkingPlaces.Add(parkingPlace);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Create();
            }
        }

        // GET: ClientsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["CarTypes"] = _context.CarTypes.Where(ct => !ct.IsDeleted).ToList();
            var place = _context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
            if (place == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(place);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int carType, IFormCollection collection)
        {
            try
            {
                var parkingPlace = _context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
                if (parkingPlace == null)
                {
                    ViewData["Message"] = "Parking place not found";
                    return Create();
                }

                var carTypeNavigation = _context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == carType); 
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                parkingPlace.CarType = carTypeNavigation.Id;
                parkingPlace.CarTypeNavigation = carTypeNavigation;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Edit(id);
            }
        }

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var parkingPlace = _context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
                if (parkingPlace != null)
                {
                    _context.ParkingPlaces.Remove(parkingPlace);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
