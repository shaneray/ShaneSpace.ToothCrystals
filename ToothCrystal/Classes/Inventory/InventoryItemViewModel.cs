
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToothCrystal.Classes.Inventory
{
    public class InventoryItemViewModel
    {
        [Key]
        public string Id { get; set; }
        [DisplayName("QTY in Stock")]
        public int QtyInStock { get; set; }
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }
        [DisplayName("My Code")]
        public int MyCode { get; set; }
        public string Color { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImageExtension { get; set; }
    }
}