using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToothCrystal.Areas.Admin.Models.FAQ;
using ToothCrystal.Classes;
using ToothCrystal.Classes.FAQ;

namespace ToothCrystal.Areas.Admin.Controllers
{
    public partial class FAQController : Controller
    {

        private IAsyncDataDocumentSession RavenSession { get; set; }
        private IFaqManager FaqManager { get; set; }

        public FAQController(IAsyncDataDocumentSession session, IFaqManager faqManager)
        {
            RavenSession = session;
            FaqManager = faqManager;
        }

        //
        // GET: /Admin/FAQ/
        public async virtual Task<ActionResult> Index()
        {
            IList<FaqObject> newList = await FaqManager.GetFaqList();
            return View(newList);
        }

        //
        // GET: /Admin/FAQ/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/FAQ/Create
        [HttpPost]
        public async virtual Task<ActionResult> Create(CreateViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                FaqObject newFaqObject = new FaqObject();
                UpdateModel(newFaqObject);
                await FaqManager.AddNewFaq(newFaqObject);
                await FaqManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/FAQ/Edit/5
        public async virtual Task<ActionResult> Edit(string id)
        {
            FaqObject model = new FaqObject();
            model = await FaqManager.GetFaq(id);

            CreateViewModel viewModel = new CreateViewModel
            {
                Id = model.Id,
                Answer = model.Answer,
                Question = model.Question
            };

            return View(viewModel);
        }

        //
        // POST: /Admin/FAQ/Edit/5
        [HttpPost]
        public async virtual Task<ActionResult> Edit(string id, CreateViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                FaqObject updatedFaqObject = new FaqObject();
                UpdateModel(updatedFaqObject);
                await FaqManager.UpdateFaq(updatedFaqObject);
                await FaqManager.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        //
        // GET: /Admin/FAQ/Delete/5
        public async virtual Task<ActionResult> Delete(string id)
        {
            FaqObject model = new FaqObject();
            model = await FaqManager.GetFaq(id);
            return View(model);
        }

        //
        // POST: /Admin/FAQ/Delete/5
        [HttpPost]
        public async virtual Task<ActionResult> Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FaqManager.DeleteFaq(id);
                await FaqManager.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
