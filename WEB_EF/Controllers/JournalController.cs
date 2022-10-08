using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class JournalController : Controller
    {
        private static readonly AutoparkContext context = new();

        // GET: JournalController
        public ActionResult Index()
        {
            return View(context.Journals.Where(j=>!j.IsDeleted).ToList());
        }

        // GET: JournalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JournalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JournalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: JournalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JournalController/Edit/5
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

        // GET: JournalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JournalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
