using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Interfaces;

namespace WebApi.Controllers
{
    public class ParkingPlacesController : Controller
    {
        private readonly IAutoparkDBContext _context;
        private readonly ICRUDlService<ParkingPlace> _service;

        public ParkingPlacesController(IAutoparkDBContext context, ICRUDlService<ParkingPlace> service)
        {
            _service = service;
            _context = context;
        }

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Include(j => j.CarTypeNavigation).ToList());
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int carType, IFormCollection collection)
        {
            try
            {
                var carTypeNavigation = _context.CarTypes.FirstOrDefault(ct => ct.Id == carType);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                var parkingPlace = new ParkingPlace()
                {
                    CarType = carTypeNavigation.Id,
                };

                _service.Create(parkingPlace);
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
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            var place = _service.GetViaIQueriable().FirstOrDefault(p => p.Id == id);
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
                var parkingPlace = _service.GetViaIQueriable().FirstOrDefault(p => p.Id == id);
                if (parkingPlace == null)
                {
                    ViewData["Message"] = "Parking place not found";
                    return Create();
                }

                var carTypeNavigation = _context.CarTypes.FirstOrDefault(ct => ct.Id == carType); 
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                parkingPlace.CarType = carTypeNavigation.Id;
                parkingPlace.CarTypeNavigation = carTypeNavigation;
                _service.Update(parkingPlace);
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
                var parkingPlace = _service.GetViaIQueriable().FirstOrDefault(p => p.Id == id);
                if (parkingPlace != null)
                {
                    _service.Delete(parkingPlace);
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
