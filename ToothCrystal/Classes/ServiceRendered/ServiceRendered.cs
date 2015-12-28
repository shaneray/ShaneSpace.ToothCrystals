
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToothCrystal.Classes.ServiceRendered
{
    public class ServiceRendered
    {
        [Key]
        public string Id { get; set; }

        [DisplayName("When")]
        public DateTime? DateTime { get; set; }

        [Required]
        public string Service { get; set; }

        [UIHint("ReadOnly")]
        public string CustomerName { get; set; }

        [UIHint("ReadOnly")]
        public string CustomerId { get; set; }

        [UIHint("Int")]
        [DisplayName("Amount Paid")]
        public string AmountPaid { get; set; }

        [UIHint("Int")]
        [DisplayName("Tip Amount")]
        public string TipAmount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}