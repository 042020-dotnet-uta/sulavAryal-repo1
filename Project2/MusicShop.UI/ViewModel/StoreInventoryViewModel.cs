using MusicShop.Domain;
using System.Collections.Generic;

namespace MusicShop.UI.ViewModel
{
    public class StoreInventoryViewModel
    {
        public IEnumerable<InventoryItem> InventoryItems { get; set; }
        public bool IsSuccess { get; set; }
    }
}
