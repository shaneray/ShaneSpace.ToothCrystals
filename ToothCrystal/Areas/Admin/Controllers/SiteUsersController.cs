using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToothCrystal.Classes;
using ToothCrystal.Models;

namespace ToothCrystal.Areas.Admin.Controllers
{
    public partial class SiteUsersController : Controller
    {
        IDataDocumentSession RavenSession { get; set; }

        public SiteUsersController(IDataDocumentSession session)
        {
            RavenSession = session;
        }

        //
        // GET: /Admin/SiteUsers/Index/
        public virtual ActionResult Index()
        {
            IList<ApplicationUser> model = RavenSession.Query<ApplicationUser>().ToList();
            return View(model);
        }


        //
        // GET: /Admin/SiteUsers/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/SiteUsers/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SiteUsers/Create
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/SiteUsers/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/SiteUsers/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/SiteUsers/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/SiteUsers/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
