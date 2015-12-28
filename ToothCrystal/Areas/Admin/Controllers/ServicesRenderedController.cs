using System.Threading.Tasks;
using System.Web.Mvc;
using ToothCrystal.Areas.Admin.Models.ServicesRendered;
using ToothCrystal.Classes.Customer;
using ToothCrystal.Classes.ServiceRendered;

namespace ToothCrystal.Areas.Admin.Controllers
{
    public partial class ServicesRenderedController : Controller
    {
        private ICustomerManager CustomerManager { get; set; }
        private IServiceRenderedManager ServiceRenderedManager { get; set; }

        public ServicesRenderedController(ICustomerManager manager, IServiceRenderedManager serviceRenderedManager)
        {
            CustomerManager = manager;
            ServiceRenderedManager = serviceRenderedManager;
        }

        //
        // GET: /Admin/ServicesRendered/
        public async virtual Task<ActionResult> Index(string id)
        {
            Customer currentCustomer = await CustomerManager.GetCustomer(id);
            ViewBag.Id = id;
            ViewBag.CustomerName = string.Format("{0} {1}", currentCustomer.FirstName, currentCustomer.LastName);
            return View(await ServiceRenderedManager.GetServiceRenderedList(id));
        }

        //
        // GET: /Admin/ServicesRendered/Details/5
        public async virtual Task<ActionResult> Details(string id)
        {
            return View(await ServiceRenderedManager.GetServiceRendered(id));
        }

        //
        // GET: /Admin/ServicesRendered/Create
        public async virtual Task<ActionResult> Create(string id)
        {
            Customer currentCustomer = await CustomerManager.GetCustomer(id);
            return View(new ServiceRenderedViewModel { CustomerId = id, CustomerName = string.Format("{0} {1}", currentCustomer.FirstName, currentCustomer.LastName) });
        }

        //
        // POST: /Admin/ServicesRendered/Create
        [HttpPost]
        public async virtual Task<ActionResult> Create(string id, ServiceRenderedViewModel model)
        {
            try
            {
                //  Build and bind
                ServiceRendered newServiceRendered = new ServiceRendered();
                UpdateModel(newServiceRendered);

                // Clean id from new obj and add customer Id
                newServiceRendered.Id = null;
                newServiceRendered.CustomerId = id;

                await ServiceRenderedManager.AddNewServiceRendered(newServiceRendered);
                await ServiceRenderedManager.SaveChanges();

                return RedirectToAction("Details", "Customer", new { id = newServiceRendered.CustomerId });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/ServicesRendered/Edit/5
        public async virtual Task<ActionResult> Edit(string id)
        {
            ServiceRendered model = await ServiceRenderedManager.GetServiceRendered(id);
            Customer currentCustomer = await CustomerManager.GetCustomer(model.CustomerId);

            ServiceRenderedViewModel viewModel = new ServiceRenderedViewModel
            {
                AmountPaid = model.AmountPaid,
                TipAmount = model.TipAmount,
                CustomerId = model.CustomerId,
                CustomerName = string.Format("{0} {1}", currentCustomer.FirstName, currentCustomer.LastName),
                Notes = model.Notes,
                Service = model.Service
            };

            return View(viewModel);
        }

        //
        // POST: /Admin/ServicesRendered/Edit/5
        [HttpPost]
        public async virtual Task<ActionResult> Edit(string id, ServiceRenderedViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                ServiceRendered currentServiceRendered = await ServiceRenderedManager.GetServiceRendered(id);
                ServiceRendered updatedServiceRendered = new ServiceRendered();
                UpdateModel(updatedServiceRendered);

                await ServiceRenderedManager.UpdateServiceRendered(updatedServiceRendered);
                await ServiceRenderedManager.SaveChanges();

                return RedirectToAction("Details", "Customer", new { id = currentServiceRendered.CustomerId });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/ServicesRendered/Delete/5
        public async virtual Task<ActionResult> Delete(string id)
        {
            return View(await ServiceRenderedManager.GetServiceRendered(id));
        }

        //
        // POST: /Admin/ServicesRendered/Delete/5
        [HttpPost]
        public async virtual Task<ActionResult> Delete(string id, ServiceRendered model)
        {
            try
            {
                // TODO: Add delete logic here
                ServiceRendered currentServiceRendered = await ServiceRenderedManager.GetServiceRendered(id);
                await ServiceRenderedManager.DeleteServiceRendered(id);
                await ServiceRenderedManager.SaveChanges();

                return RedirectToAction("Details", "Customer", new { id = currentServiceRendered.CustomerId });
            }
            catch
            {
                return View();
            }
        }
    }
}
