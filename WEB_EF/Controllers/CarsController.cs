using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace WEB_EF.Controllers
{
    public class CarsController : Controller
    {
        private static readonly AutoparkContext context = new();

        public ActionResult Index()
        {
            return View(context.Cars.Include(c => c.Client).Include(c => c.CarTypeNavigation).Where(j => !j.IsDeleted).ToList());
        }

        public ActionResult Create()
        {
            ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string regNumber, int clientID, int carType, IFormCollection collection)
        {
            try
            {

                var client = context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Create();
                }

                var carTypeNavigation = context.CarTypes.FirstOrDefault(c => c.Id == carType && !c.IsDeleted);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                context.Cars.Add(new Car { RegNumber = regNumber, CarType = carType, CarTypeNavigation = carTypeNavigation, Client = client, ClientId = client.Id });
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var obj = context.Cars.FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View(obj);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string regNumber, int clientID, int carType, IFormCollection collection)
        {
            try
            {
                var client = context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Edit(id);
                }

                var carTypeNavigation = context.CarTypes.FirstOrDefault(c => c.Id == carType && !c.IsDeleted);
                if (carTypeNavigation == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Edit(id);
                }

                var car = context.Cars.First(c => c.Id == id);
                car.RegNumber = regNumber;
                car.Client = client;
                car.ClientId = client.Id;
                car.CarTypeNavigation = carTypeNavigation;
                car.CarType = carType;
                context.SaveChanges();
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
                var car = context.Cars.First(c => c.Id == id);
                context.Cars.Remove(car);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
