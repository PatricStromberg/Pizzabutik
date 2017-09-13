using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [DisplayName("City")]
        public string ShippingCity { get; set; }

        [Required]
        [DisplayName("Zip")]
        public int ShippingZip { get; set; }

        [Required]
        [DisplayName("Phone")]
        public int HomePhone { get; set; }
    }
}
