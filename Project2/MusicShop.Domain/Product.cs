using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicShop.Domain
{
    public class Product
    {
        /// <summary>
        /// Consturctor that takes in no parameters, and initilizes the ICollection of Product
        /// </summary>
        public Product()
        {
            // This has to be initialized to cover for the cases where, you may want to use empty Orders
            // with no InventoryItem. 
            this.Inventory = new HashSet<InventoryItem>();
        }
        /// <summary>
        /// Gets or Sets Primary key of Product 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or Sets Product name 
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or Sets Product code
        /// </summary>
        public string  ProductCode { get; set; }
        /// <summary>
        /// Gets or Sets Product price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or Sets Navigational property of Inventory
        /// Its the many end of the one to many relationship between 
        /// InventoryItems and Product.
        /// </summary>

        public ICollection<InventoryItem> Inventory{get;set;}

    }
}