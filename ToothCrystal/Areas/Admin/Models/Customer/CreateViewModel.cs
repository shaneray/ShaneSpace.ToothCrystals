using System.ComponentModel.DataAnnotations;
using ToothCrystal.Classes.Customer;

namespace ToothCrystal.Areas.Admin.Models.Customer
{
    public class CreateViewModel
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}