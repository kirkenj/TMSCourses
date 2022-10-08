using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class ClientsController : Controller
    {
        private static readonly AutoparkContext context = new();

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(context.Clients.Where(c=>!c.IsDeleted).ToList());
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
            try
            {
                var name = collection["Name"].ToString();
                var surname = collection["Surname"].ToString();
                if (context.Clients.Any(c => c.Name == name && surname == c.Surname))
                {
                    ViewData["Message"] = $"Client {name} {surname} already excists";
                    return View();
                }

                context.Clients.Add(new Client { Name = name, Surname = surname });
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var obj = context.Clients.First(ct => ct.Id == id && !ct.IsDeleted);
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
                var name = collection["Name"].ToString();
                var surname = collection["Surname"].ToString();
                var fc = context.Clients.FirstOrDefault(c => c.Name == name && surname == c.Surname);
                if (fc != null)
                {
                    ViewData["Message"] = $"Client {name} {surname} already excists";
                    return View(fc);
                }

                var obj = context.Clients.First(cl => !cl.IsDeleted && cl.Id == id);
                obj.Name = name;
                obj.Surname = surname;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                var obj = context.Clients.First(ct => !ct.IsDeleted && ct.Id == id);
                context.Clients.Remove(obj);
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
