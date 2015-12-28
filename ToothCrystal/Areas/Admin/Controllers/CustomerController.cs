using System.Threading.Tasks;
using System.Web.Mvc;
using ToothCrystal.Areas.Admin.Models.Customer;
using ToothCrystal.Classes.Customer;
using ToothCrystal.Classes.ServiceRendered;

namespace ToothCrystal.Areas.Admin.Controllers
{
    public partial class CustomerController : Controller
    {
        private ICustomerManager CustomerManager { get; set; }
        private IServiceRenderedManager ServiceRenderedManager { get; set; }

        public CustomerController(ICustomerManager manager, IServiceRenderedManager serviceRenderedManager)
        {
            CustomerManager = manager;
            ServiceRenderedManager = serviceRenderedManager;
        }

        //
        // GET: /Admin/Customer/
        public async virtual Task<ActionResult> Index()
        {
            return View(await CustomerManager.GetCustomerList());
        }

        //
        // GET: /Admin/Customer/Details/5
        public async virtual Task<ActionResult> Details(string id)
        {
            ViewBag.ServicesRendered = await ServiceRenderedManager.GetServiceRenderedList(id);
            ViewBag.CustomerId = id;
            return View(await CustomerManager.GetCustomer(id));
        }

        //
        // GET: /Admin/Customer/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Customer/Create
        [HttpPost]
        public async virtual Task<ActionResult> Create(CreateViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                Customer newCustomer = new Customer();
                UpdateModel(newCustomer);
                await CustomerManager.CreateCustomer(newCustomer);
                await CustomerManager.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Customer/Edit/5
        public async virtual Task<ActionResult> Edit(string id)
        {
            return View(await CustomerManager.GetCustomer(id));
        }

        //
        // POST: /Admin/Customer/Edit/5
        [HttpPost]
        public async virtual Task<ActionResult> Edit(string id, CreateViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                await CustomerManager.UpdateCustomer(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Customer/Delete/5
        public async virtual Task<ActionResult> Delete(string id)
        {
            return View(await CustomerManager.GetCustomer(id));
        }

        //
        // POST: /Admin/Customer/Delete/5
        [HttpPost]
        public async virtual Task<ActionResult> Delete(string id, Customer model)
        {
            try
            {
                // TODO: Add delete logic here
                CustomerManager.DeleteCustomer(id);
                await CustomerManager.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
