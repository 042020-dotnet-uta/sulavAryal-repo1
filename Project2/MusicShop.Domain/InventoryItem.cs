using System;

namespace MusicShop.Domain
{
    public class InventoryItem
    {
        /// <summary>
        /// Gets or sets the primary key of InventoryItem
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Quantity of InventoryItem
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the Logged User Id of InventoryItem
        /// </summary>
        public int LoggedUserId { get; set; }
        /// <summary>
        /// Gets or sets the first name of InventoryItem
        /// </summary>
        public DateTimeOffset? ChangedDate { get; set; }
        /// <summary>
        /// Gets or sets the store Id for InventoryItem, is a foreign key.
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// Gets or sets the navigational property of Inventory Item
        /// </summary>
        public Store Store { get; set; }
        /// <summary>
        /// Gets or sets Product Id for Inventory Item, is a froeign key.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the navigational property of Inventory Item
        /// </summary>
        public Product Product { get; set; }
    }
}
