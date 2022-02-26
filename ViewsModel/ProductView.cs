﻿using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class ProductView 
    {
        public int Id { get; set; }

        //public int MasterProductId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        //public int CatagoryId { get; set; }

        public string? Size { get; set; }

        public double? Price { get; set; }

        public int? Gender { get; set; }

        public string? Color { get; set; }

        public int? Inventory { get; set; }

    }
}