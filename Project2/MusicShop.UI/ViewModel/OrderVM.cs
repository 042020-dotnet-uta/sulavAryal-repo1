using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.UI
{
    public class OrderVM
    {
        public int CustomerId { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
