using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
