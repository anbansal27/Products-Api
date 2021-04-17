﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Products.Api.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal? DeliveryPrice { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<ProductOption> ProductOptions { get; set; }
    }
}
