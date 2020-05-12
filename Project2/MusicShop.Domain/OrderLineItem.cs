using System;
using System.Collections.Generic;
using System.Text;

namespace MusicShop.Domain
{
    public class OrderLineItem
    {
        /// <summary>
        /// Gets or sets Primary key of OrderLineItem
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets Quantity of OrderLineItem
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets Price at the time of OrderLineItem creation
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets OrderId of OrderLineItem, is a foreign key.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Gets or sets Navigational Property of Order
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// Gets or sets InventoryItem Id of OrderLineItem 
        /// </summary>
        public int InventoryItemId { get; set; }
    }
}
