using System;
using System.Collections.Generic;

namespace MusicShop.Domain
{
    public class Order
    {
        /// <summary>
        /// Consturctor that takes in no parameters, and initilizes the ICollection of OrderLineItems
        /// </summary>
        public Order()
        {
            // This has to be initialized to cover for the cases where, you may want to use empty Orders
            // with no OrderLineItems. 
            this.OrderLineItems = new HashSet<OrderLineItem>();
        }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public DateTimeOffset? OrderDate { get; set; }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public Store Store { get; set; }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets Primary key of Order 
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// Gets or sets ICollection navigational property of OrderLineItems 
        /// This is the (many) end of the one to many relationship 
        /// between Order entity and OrderLineItems entity
        /// </summary>
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
    }
}
