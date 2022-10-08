using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class CarsController : Controller
    {
        private static readonly AutoparkContext context = new();

        // GET: ClientsController
        public ActionResult Index()
        {
            ViewData["Clients"] = context.Clients.ToList();
            ViewData["CarTypes"] = context.CarTypes.ToList();
            return View(context.Cars.Where(j => !j.IsDeleted).ToList());
        }

        public ActionResult Create()
        {
            ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
            ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var regNumber = collection["regNumber"].ToString();
                var clientIDStr = collection["ClientID"].ToString();
                var carTypeIDStr = collection["CarType"].ToString();

                if (context.Cars.Any(c=> c.RegNumber == regNumber))
                {
                    ViewData["Message"] = "Car with this registration number already excists";
                    return Create();
                }

                if (!int.TryParse(clientIDStr, out int clientID))
                {
                    ViewData["Message"] = "Invalid client ID";
                    return Create();
                }

                if (!int.TryParse(carTypeIDStr, out int carTypeID))
                {
                    ViewData["Message"] = "Invalid car type ID";
                    return Create();
                }

                var client = context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Create();
                }

                var carType = context.CarTypes.FirstOrDefault(c => c.Id == carTypeID && !c.IsDeleted);
                if (carType == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                context.Cars.Add(new Car { RegNumber = regNumber, CarType = carType.Id, CarTypeNavigation = carType, Client = client, ClientId = client.Id });
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
            try
            {
                var obj = context.Cars.First(ct => ct.Id == id && !ct.IsDeleted);
                ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                return View(obj);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var regNumber = collection["regNumber"].ToString();
                var clientIDStr = collection["ClientID"].ToString();
                var carTypeIDStr = collection["CarType"].ToString();

                if (context.Cars.Any(c => c.RegNumber == regNumber && c.Id != id))
                {
                    ViewData["Message"] = "Car with this registration number already excists";
                    return Edit(id);
                }

                if (!int.TryParse(clientIDStr, out int clientID))
                {
                    ViewData["Message"] = "Invalid client ID";
                    return Edit(id);
                }

                if (!int.TryParse(carTypeIDStr, out int carTypeID))
                {
                    ViewData["Message"] = "Invalid car type ID";
                    return Edit(id);
                }

                var client = context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Message"] = "Client not found";
                    return Edit(id);
                }

                var carType = context.CarTypes.FirstOrDefault(c => c.Id == carTypeID && !c.IsDeleted);
                if (carType == null)
                {
                    ViewData["Message"] = "Car type not found";
                    return Edit(id);
                }

                var car = context.Cars.First(c => c.Id == id);
                car.RegNumber = regNumber;
                car.Client = client;
                car.ClientId = client.Id;
                car.CarTypeNavigation = carType;
                car.CarType = carTypeID;
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
