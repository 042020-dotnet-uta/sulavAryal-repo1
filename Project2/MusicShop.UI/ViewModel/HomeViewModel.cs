using MusicShop.Domain;
using System.Collections.Generic;

namespace MusicShop.UI.ViewModel
{
    public class HomeViewModel
    {
        /// <summary>
        /// Gets and get IEnumerable of type InventoryItem
        /// </summary>
        public IEnumerable<InventoryItem> Products { get; set; }
        /// <summary>
        /// Gets and sets storeId
        /// </summary>
        public int? StoreId { get; set; }
    }
}
