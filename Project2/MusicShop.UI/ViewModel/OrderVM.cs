using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.UI
{
    public class OrderVM
    {
        /// <summary>
        /// Gets and sets CustomerId
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets and sets Custome Name for order
        /// </summary>
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        /// <summary>
        /// Gets and sets product id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets and sets product name
        /// </summary>
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        /// <summary>
        /// Gets and sets IEnumerable of SelecListItems
        /// </summary>
        public IEnumerable<SelectListItem> Products { get; set; }
        /// <summary>
        /// Gets and sets Quantity 
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets price 
        /// </summary>
        public decimal Price { get; set; }
    }
}
