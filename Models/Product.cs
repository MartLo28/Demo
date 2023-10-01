using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        // Add more properties as needed
    }
}
