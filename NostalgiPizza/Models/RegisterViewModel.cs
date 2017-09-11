using System;
using System.Collections.Generic;
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
        public string ShippingAddress { get; set; }

        [Required]
        public string ShippingCity { get; set; }

        [Required]
        public int ShippingZip { get; set; }

        [Required]
        public int HomePhone { get; set; }
    }
}
