using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.DBContexts;
using WEB_EF.Models.Classes;

namespace WEB_EF.Controllers
{
    public class ParkingPlacesController : Controller
    {
        private static readonly AutoparkContext context = new();

        // GET: ClientsController
        public ActionResult Index()
        {
            ViewData["CarTypes"] = context.CarTypes.Where(ct => !ct.IsDeleted).ToList();
            return View(context.ParkingPlaces.Where(j => !j.IsDeleted).ToList());
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            ViewData["CarTypes"] = context.CarTypes.Where(ct=>!ct.IsDeleted).ToList();
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var carTypeStr = collection["CarType"];
                if (string.IsNullOrEmpty(carTypeStr) || !int.TryParse(carTypeStr, out int carTypeId))
                {
                    ViewData["Message"] = "Invalid car type";
                    return Create();
                }

                var parkingPlace = new ParkingPlace();
                var carType = context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == carTypeId);
                if (carType == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                parkingPlace.CarType = carType.Id;
                parkingPlace.CarTypeNavigation = carType;
                context.ParkingPlaces.Add(parkingPlace);
                context.SaveChanges();
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
            ViewData["CarTypes"] = context.CarTypes.Where(ct => !ct.IsDeleted).ToList();
            var place = context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
            if (place == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(place);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var carTypeStr = collection["CarType"];
                if (string.IsNullOrEmpty(carTypeStr) || !int.TryParse(carTypeStr, out int carTypeId))
                {
                    ViewData["Message"] = "Invalid car type";
                    return Edit(id);
                }

                var parkingPlace = context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
                if (parkingPlace == null)
                {
                    ViewData["Message"] = "Parking place not found";
                    return Create();
                }

                var carType = context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == carTypeId); 
                if (carType == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                parkingPlace.CarType = carType.Id;
                parkingPlace.CarTypeNavigation = carType;
                context.SaveChanges();
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
                var parkingPlace = context.ParkingPlaces.FirstOrDefault(p => !p.IsDeleted && p.Id == id);
                if (parkingPlace != null)
                {
                    context.ParkingPlaces.Remove(parkingPlace);
                    context.SaveChanges();
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
