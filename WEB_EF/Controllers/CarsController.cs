using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace WEB_EF.Controllers
{
    public class CarsController : Controller
    {
        private readonly AutoparkContext _context;

        public CarsController(AutoparkContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Cars.Include(c => c.Client).Include(c => c.CarTypeNavigation).Where(j => !j.IsDeleted).ToList());
        }

        public ActionResult Create()
        {
            ViewData["Clients"] = _context.Clients.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = _context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string regNumber, int clientID, int carType, IFormCollection collection)
        {
            try
            {

                var client = _context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Create();
                }

                var carTypeNavigation = _context.CarTypes.FirstOrDefault(c => c.Id == carType && !c.IsDeleted);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                _context.Cars.Add(new Car { RegNumber = regNumber, CarType = carType, CarTypeNavigation = carTypeNavigation, Client = client, ClientId = client.Id });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var obj = _context.Cars.FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["Clients"] = _context.Clients.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = _context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View(obj);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string regNumber, int clientID, int carType, IFormCollection collection)
        {
            try
            {
                var client = _context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Edit(id);
                }

                var carTypeNavigation = _context.CarTypes.FirstOrDefault(c => c.Id == carType && !c.IsDeleted);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Edit(id);
                }

                var car = _context.Cars.First(c => c.Id == id);
                car.RegNumber = regNumber;
                car.Client = client;
                car.ClientId = client.Id;
                car.CarTypeNavigation = carTypeNavigation;
                car.CarType = carType;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var car = _context.Cars.First(c => c.Id == id);
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
