﻿using System.Text.Json.Serialization;

namespace SimpleInventory.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<Product>? Products { get; set; }

    }
}
