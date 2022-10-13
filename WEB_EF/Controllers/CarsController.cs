using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using Microsoft.EntityFrameworkCore;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICRUDleService<Car> _service;
        private readonly IAutoparkDBContext _context;

        public CarsController(ICRUDleService<Car> CRUDLService, IAutoparkDBContext context)
        {
            _context = context;
            _service = CRUDLService;
        }

        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Include(c => c.Client).Include(c => c.CarTypeNavigation).Where(j => !j.IsDeleted).ToList());
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
                if (!_context.Clients.Any(c => c.Id == clientID && !c.IsDeleted))
                {
                    ViewData["Message"] = "Client not found";
                    return Create();
                }

                if (!_context.CarTypes.Any(c => c.Id == carType && !c.IsDeleted))
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
            var obj = _service.GetViaIQueriable().FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
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
                if (!_context.Clients.Any(c => c.Id == clientID && !c.IsDeleted))
                {
                    ViewData["Message"] = "Client not found";
                    return Edit(id);
                }

                if (!_context.CarTypes.Any(c => c.Id == carType && !c.IsDeleted))
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
