using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NostalgiPizza.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public int ShippingZip { get; set; }
        public string HomeEmail { get; set; }
        public int HomePhone { get; set; }
        public long CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVS { get; set; }
    }
}
