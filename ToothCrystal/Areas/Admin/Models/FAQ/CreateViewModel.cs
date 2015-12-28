
using System.ComponentModel.DataAnnotations;

namespace ToothCrystal.Areas.Admin.Models.FAQ
{
    public class CreateViewModel
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }
    }
}