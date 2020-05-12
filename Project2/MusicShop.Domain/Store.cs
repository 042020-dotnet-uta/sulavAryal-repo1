using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicShop.Domain
{
    public class Store
    {
        /// <summary>
        /// Consturctor that takes in no parameters, and initilizes the ICollection of Store
        /// </summary>
        public Store()
        {
            // This has to be initialized to cover for the cases where, you may want to use empty Store
            // with no InventoryItem. 
            this.Inventory = new HashSet<InventoryItem>();
        }
        /// <summary>
        /// Gets or sets Primary key of Store
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of Store
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets Naviagation property of Inventory
        /// many half of the one to many relationship between 
        /// InventoryItem and store.
        /// as a store can have many InventoryItems. 
        /// </summary>
        public ICollection<InventoryItem> Inventory { get; set; }
    }
}
