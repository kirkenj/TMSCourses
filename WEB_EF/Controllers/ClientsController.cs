using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IAutoparkDBContext _context;
        private readonly ICRUDlService<Client> _service;

        public ClientsController(IAutoparkDBContext context, ICRUDlService<Client> service)
        {
            _context = context;
            _service = service;
        }

        // GET: ClientsController
        public ActionResult Index()
        {
            return View(_service.GetViaIQueriable().Where(c=>!c.IsDeleted).ToList());
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
                if (_service.GetViaIQueriable().Any(c => c.Name == name && surname == c.Surname))
                {
                    ViewData["Message"] = $"Client {name} {surname} already excists";
                    return View();
                }

                _service.Create(new Client { Name = name, Surname = surname });
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
            var obj = _service.GetViaIQueriable().FirstOrDefault(ct => ct.Id == id && !ct.IsDeleted);
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
                if (_service.GetViaIQueriable().Any(c => c.Id != id && c.Name == name && surname == c.Surname))
                {
                    ViewData["Message"] = $"Client {name} {surname} already excists";
                    return Edit(id);
                }

                var obj = _service.GetViaIQueriable().FirstOrDefault(cl => !cl.IsDeleted && cl.Id == id);
                if (obj == null)
                {
                    ViewData["Message"] = $"Client not found";
                    return Edit(id);
                }

                obj.Name = name;
                obj.Surname = surname;
                _service.Update(obj);
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
                var obj = _service.GetViaIQueriable().FirstOrDefault(ct => !ct.IsDeleted && ct.Id == id);
                if (obj != null)
                {
                    _service.Delete(obj);
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
