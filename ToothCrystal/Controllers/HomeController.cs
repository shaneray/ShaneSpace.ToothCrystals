using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToothCrystal.Classes.FAQ;
using ToothCrystal.Classes.Inventory;

namespace ToothCrystal.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IFaqManager _faqManager;
        private readonly IInventoryManager _inventoryManager;

        public HomeController(IFaqManager faqManager, IInventoryManager inventoryManager)
        {
            _faqManager = faqManager;
            _inventoryManager = inventoryManager;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            return View();
        }

        public async virtual Task<ActionResult> Faq()
        {
            IList<FaqObject> newList = await _faqManager.GetFaqList();
            return View(newList);
        }

        public virtual ActionResult BodyJewelry()
        {
            return View();
        }

        public async virtual Task<ActionResult> Inventory()
        {
            IList<InventoryItem> newList = await _inventoryManager.GetInventoryList();
            return View(newList);
        }
    }
}