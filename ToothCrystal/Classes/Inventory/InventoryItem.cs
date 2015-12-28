
using System.ComponentModel;

namespace ToothCrystal.Classes.Inventory
{
    public class InventoryItem
    {
        public string Id { get; set; }
        [DisplayName("Quantity in Stock")]
        public int QtyInStock { get; set; }
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }
        [DisplayName("My Code")]
        public int MyCode { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string ImageExtension { get; set; }
    }
}