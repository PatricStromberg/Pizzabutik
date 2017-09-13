using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NostalgiPizza.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("Firstname")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Lastname")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string ShippingAddress { get; set; }
        [Required]
        [DisplayName("City")]
        public string ShippingCity { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        [DisplayName("Zip")]
        public int ShippingZip { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string HomeEmail { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [DisplayName("Phone")]
        public int HomePhone { get; set; }
        [Required(ErrorMessage = "Cardnumber is required")]
        [DisplayName("Cardnumber")]
        public long CardNumber { get; set; }
        [Required(ErrorMessage = "Exp month is required")]
        [DisplayName("Exp month")]
        public int ExpirationMonth { get; set; }
        [Required(ErrorMessage = "Exp year is required")]
        [DisplayName("Exp year")]
        public int ExpirationYear { get; set; }
        [Required(ErrorMessage = "CVS is required")]
        [DisplayName("CVS")]
        public int CVS { get; set; }
    }
}
