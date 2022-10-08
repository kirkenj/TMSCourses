using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class CarTypesController : Controller
    {
        private static readonly AutoparkContext context = new();

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(context.CarTypes.Where(j => !j.IsDeleted));
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var tname = collection["TypeName"].ToString();
            try
            {
                if (context.CarTypes.Any(ct => ct.TypeName == tname))
                {
                    ViewData["Message"] = $"This value({tname}) already excists";
                    return View();
                }

                context.CarTypes.Add(new() { TypeName = tname });
                context.SaveChanges();
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
            try
            {
                var obj = context.CarTypes.First(ct => ct.Id == id && !ct.IsDeleted);
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
                var obj = context.CarTypes.First(ct => !ct.IsDeleted && ct.Id == id);
                var tName = collection["TypeName"].ToString().Trim();
                if (context.CarTypes.Any(ct=>ct.TypeName == collection["TypeName"].ToString().Trim()))
                {
                    ViewData["Message"] = $"This value({tName}) already excists";
                    return View(obj);
                }

                obj.TypeName = tName;
                context.SaveChanges();
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
                var obj = context.CarTypes.First(ct => !ct.IsDeleted && ct.Id == id);
                context.CarTypes.Remove(obj);
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
