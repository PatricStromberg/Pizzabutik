﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
