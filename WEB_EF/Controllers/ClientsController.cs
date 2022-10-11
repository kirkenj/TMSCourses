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
        public ActionResult Create(string name, string surname, IFormCollection collection)
        {
            try
            {
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
            var obj = context.Clients.FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string name, string surname, IFormCollection collection)
        {
            try
            {
                if (context.Clients.Any(c => c.Id != id && c.Name == name && surname == c.Surname))
                {
                    ViewData["Message"] = $"Client {name} {surname} already excists";
                    return Edit(id);
                }

                var obj = context.Clients.FirstOrDefault(cl => !cl.IsDeleted && cl.Id == id);
                if (obj == null)
                {
                    ViewData["Message"] = $"Client not found";
                    return Edit(id);
                }
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
                var obj = context.Clients.FirstOrDefault(ct => !ct.IsDeleted && ct.Id == id);
                if (obj != null)
                {
                    context.Clients.Remove(obj);
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Index();
            }
        }
    }
}
