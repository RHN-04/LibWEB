using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibWEB.Controllers
{
    public class PreordersController : Controller
    {
        // GET: PreordersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreordersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreordersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreordersController/Create
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

        // GET: PreordersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreordersController/Edit/5
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

        // GET: PreordersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreordersController/Delete/5
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
