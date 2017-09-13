using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class PaymentViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public Cart Cart { get; set; }
        public int Total { get; set; }
    }
}
