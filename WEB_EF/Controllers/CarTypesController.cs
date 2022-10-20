using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;
using WebApi.Models.DBContexts;
using WebApi.Models.Interfaces;

namespace WebApi.Controllers
{
    public class CarTypesController : Controller
    {
        private readonly ICRUDlService<CarType> _service;
        private readonly IAutoparkDBContext _context;

        public CarTypesController(IAutoparkDBContext context, ICRUDlService<CarType> service)
        {
            _context = context;
            _service = service;
        }

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Include(c => c.Cars).Include(c => c.ParkingPlaces).ToList());
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string typeName, IFormCollection collection)
        {
            try
            {
                if (_service.GetViaIQueriable().Any(ct => ct.TypeName == typeName))
                {
                    ViewData["Message"] = $"This value({typeName}) already excists";
                    return View();
                }

                _service.Create(new() { TypeName = typeName });
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View();
            }
        }

        // GET: ClientsController/Edit/5
        public ActionResult Edit(int id)
        {
            var obj = _service.GetViaIQueriable().FirstOrDefault(ct => ct.Id == id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string typeName, IFormCollection collection)
        {
            try
            {
                var obj = _service.GetViaIQueriable().FirstOrDefault(ct => ct.Id == id);
                if (obj == null)
                {
                    ViewData["Message"] = $"Car not found";
                    return View(obj);
                }

                typeName = typeName.Trim();
                if (_service.GetViaIQueriable().Any(ct=>ct.Id != id && ct.TypeName == collection["TypeName"].ToString().Trim()))
                {
                    ViewData["Message"] = $"This value({typeName}) already excists";
                    return View(obj);
                }

                obj.TypeName = typeName;
                _service.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: ClientsController/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var obj = _context.CarTypes.FirstOrDefault(ct => ct.Id == id);
                if (obj != null)
                {
                    _service.Delete(obj);
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
