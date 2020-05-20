using MusicShop.Domain;
using System.Collections.Generic;

namespace MusicShop.UI.ViewModel
{
    public class StoreInventoryViewModel
    {
        /// <summary>
        /// Gets, Sets IEnumerable of InventoryItme type
        /// </summary>
        public IEnumerable<InventoryItem> InventoryItems { get; set; }
        /// <summary>
        /// Gets sets IsSuccess bool type
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
