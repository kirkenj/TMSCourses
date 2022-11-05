using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Entities;
using Microsoft.EntityFrameworkCore;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICRUDlService<Car> _service;
        private readonly IAutoparkDBContext _context;

        public CarsController(ICRUDlService<Car> CRUDLService, IAutoparkDBContext context)
        {
            _context = context;
            _service = CRUDLService;
        }

        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Include(c => c.Client).Include(c => c.CarTypeNavigation).ToList());
        }

        public ActionResult Create()
        {
            ViewData["Clients"] = _context.Clients.ToList();
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string regNumber, int? clientID, int carType, IFormCollection collection)
        {
            try
            {
                if (clientID != null && !_context.Clients.Any(c => c.Id == clientID))
                {
                    ViewData["Message"] = "Client not found";
                    return Create();
                }

                if (!_context.CarTypes.Any(c => c.Id == carType))
                {
                    ViewData["Message"] = "Car type not found";
                    return Create();
                }

                _service.Create(new Car { RegNumber = regNumber, CarType = carType, ClientId = clientID });
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Create();
            }
        }

        public ActionResult Edit(int id)
        {
            var obj = _service.GetViaIQueriable().FirstOrDefault(ct => ct.Id == id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["Clients"] = _context.Clients.ToList();
            ViewData["CarTypes"] = _context.CarTypes.ToList();
            return View(obj);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string regNumber, int? clientID, int carType, IFormCollection collection)
        {
            try
            {
                if (clientID != null && !_context.Clients.Any(c => c.Id == clientID))
                {
                    ViewData["Message"] = "Client not found";
                    return Edit(id);
                }

                if (!_context.CarTypes.Any(c => c.Id == carType))
                {
                    ViewData["Message"] = "Car type not found";
                    return Edit(id);
                }

                var car = _service.GetViaIQueriable().First(c => c.Id == id);
                car.RegNumber = regNumber;
                car.ClientId = clientID;
                car.CarType = carType;
                _service.Update(car);
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
                var car = _service.GetViaIQueriable().First(c => c.Id == id);
                _service.Delete(car);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
