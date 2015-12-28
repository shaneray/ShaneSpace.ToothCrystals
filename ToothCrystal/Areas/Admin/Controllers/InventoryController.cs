using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToothCrystal.Classes.Inventory;

namespace ToothCrystal.Areas.Admin.Controllers
{
    public partial class InventoryController : Controller
    {
        private IInventoryManager InventoryManager { get; set; }

        public InventoryController(IInventoryManager manager)
        {
            InventoryManager = manager;
        }

        //
        // GET: /Admin/Inventory/
        public async virtual Task<ActionResult> Index()
        {
            return View(await InventoryManager.GetInventoryList());
        }

        //
        // GET: /Admin/Inventory/Details/5
        public async virtual Task<ActionResult> Details(string id)
        {
            return View(await InventoryManager.GetInventoryItem(id));
        }

        //
        // GET: /Admin/Inventory/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Inventory/Create
        [HttpPost]
        public async virtual Task<ActionResult> Create(InventoryItemViewModel model, HttpPostedFileBase imageFile)
        {
            try
            {
                // TODO: Add insert logic here
                var exist = await InventoryManager.GetInventoryList();
                if (exist.Any(x => x.ProductCode == model.ProductCode))
                {
                    throw new Exception("Product ID already exist!");
                }


                InventoryItem newInventoryItem = new InventoryItem();
                UpdateModel(newInventoryItem);
                string inventoryItemId = await InventoryManager.AddNewInventoryItem(newInventoryItem);

                if (imageFile != null)
                {
                    string imgExt = UploadImageFile(imageFile, inventoryItemId);
                    newInventoryItem.ImageExtension = imgExt;
                    await InventoryManager.AddNewInventoryItem(newInventoryItem);
                }

                await InventoryManager.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Index");
                //                return View();
            }
        }

        //
        // GET: /Admin/Inventory/Edit/5
        public async virtual Task<ActionResult> Edit(string id)
        {
            InventoryItem newInventoryItem = await InventoryManager.GetInventoryItem(id);

            InventoryItemViewModel model = new InventoryItemViewModel
            {
                Id = newInventoryItem.Id,
                Color = newInventoryItem.Color,
                QtyInStock = newInventoryItem.QtyInStock,
                Description = newInventoryItem.Description,
                ProductCode = newInventoryItem.ProductCode,
                ImageExtension = newInventoryItem.ImageExtension,
                MyCode = newInventoryItem.MyCode
            };

            return View(model);
        }

        //
        // POST: /Admin/Inventory/Edit/5
        [HttpPost]
        public async virtual Task<ActionResult> Edit(string id, InventoryItemViewModel model, HttpPostedFileBase imageFile)
        {
            try
            {
                // TODO: Add update logic here
                InventoryItem newInventoryItem = new InventoryItem();
                UpdateModel(newInventoryItem);
                string inventoryItemId = await InventoryManager.UpdateInventoryItem(newInventoryItem);

                if (imageFile != null)
                {
                    DeleteImageFile(string.Format("{0}{1}", id, model.ImageExtension));
                    string imgExt = UploadImageFile(imageFile, newInventoryItem.Id);
                    newInventoryItem.ImageExtension = imgExt;
                    await InventoryManager.UpdateInventoryItem(newInventoryItem);
                }

                await InventoryManager.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(model);
            }
        }

        //
        // GET: /Admin/Inventory/Delete/5
        public async virtual Task<ActionResult> Delete(string id)
        {
            return View(await InventoryManager.GetInventoryItem(id));
        }

        //
        // POST: /Admin/Inventory/Delete/5
        [HttpPost]
        public async virtual Task<ActionResult> Delete(string id, InventoryItem model)
        {
            try
            {
                // TODO: Add delete logic here
                await InventoryManager.DeleteInventoryItem(id);
                await InventoryManager.SaveChanges();
                DeleteImageFile(string.Format("{0}{1}", model.Id, model.ImageExtension));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Supporting Methods
        private string UploadImageFile(HttpPostedFileBase file, string newFileName)
        {
            string[] supportedExtentsions = new[] { ".jpg", ".jpeg", ".gif", ".png" };

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(fileName);

                if (!supportedExtentsions.Contains(extension.ToLower()))
                {
                    throw new ArgumentException(string.Format("File Format not supported.  Supported formats are {0}", string.Join(",", supportedExtentsions)));
                }

                var path = Path.Combine(Server.MapPath("~/Content/Images/Inventory"), string.Format("{0}{1}", newFileName, extension));
                file.SaveAs(path);
                return extension;
            }
            return null;

        }
        private void DeleteImageFile(string filename)
        {
            filename = string.Format(@"{1}Content\Images\Inventory\{0}", filename, HttpRuntime.AppDomainAppPath);

            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
        }

        public static string GetImageFile(string filename)
        {
            var file = string.Format("/Content/images/Inventory/{0}", filename);
            if (System.IO.File.Exists(HttpRuntime.AppDomainAppPath + file))
            {
                return string.Format("/Content/Images/Inventory/{0}", filename);
            }
            else
            {
                return string.Format("/Content/Images/Inventory/{0}", "noImage.jpg");
            }
        }
    }
}
