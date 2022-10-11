using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class CarTypesController : Controller
    {
        private readonly AutoparkContext _context;

        public CarTypesController(AutoparkContext context)
        {
            _context = context;
        }

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(_context.CarTypes.Where(ct => !ct.IsDeleted).Include(c => c.Cars.Where(c=>!c.IsDeleted)).Include(c => c.ParkingPlaces.Where(c => !c.IsDeleted)).ToList());
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
                if (_context.CarTypes.Any(ct => ct.TypeName == typeName))
                {
                    ViewData["Message"] = $"This value({typeName}) already excists";
                    return View();
                }

                _context.CarTypes.Add(new() { TypeName = typeName });
                _context.SaveChanges();
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
            var obj = _context.CarTypes.FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
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
                var obj = _context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == id);
                if (obj == null)
                {
                    ViewData["Message"] = $"Car not found";
                    return View(obj);
                }

                typeName = typeName.Trim();
                if (_context.CarTypes.Any(ct=>ct.Id != id && ct.TypeName == collection["TypeName"].ToString().Trim()))
                {
                    ViewData["Message"] = $"This value({typeName}) already excists";
                    return View(obj);
                }

                obj.TypeName = typeName;
                _context.SaveChanges();
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
                var obj = _context.CarTypes.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == id);
                if (obj != null)
                {
                    _context.CarTypes.Remove(obj);
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
