﻿using Microsoft.AspNetCore.Http;
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
                    ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                    ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                    ViewData["Message"] = "Car with this registration number already excists";
                    return View();
                }

                if (!int.TryParse(clientIDStr, out int clientID))
                {
                    ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                    ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                    ViewData["Message"] = "Invalid client ID";
                    return View();
                }

                if (!int.TryParse(carTypeIDStr, out int carTypeID))
                {
                    ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                    ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                    ViewData["Message"] = "Invalid car type ID";
                    return View();
                }

                var client = context.Clients.FirstOrDefault(c => c.Id == clientID && !c.IsDeleted);
                if (client == null)
                {
                    ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                    ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                    ViewData["Message"] = "Client not found";
                    return View();
                }

                var carType = context.CarTypes.FirstOrDefault(c => c.Id == carTypeID && !c.IsDeleted);
                if (carType == null)
                {
                    ViewData["Clients"] = context.Clients.Where(c => !c.IsDeleted).ToList();
                    ViewData["CarTypes"] = context.CarTypes.Where(c => !c.IsDeleted).ToList();
                    ViewData["Message"] = "Car type not found";
                    return View();
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
            return View();
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
