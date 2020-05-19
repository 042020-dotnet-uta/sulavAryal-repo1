using MusicShop.Domain;
using System.Collections.Generic;

namespace MusicShop.UI.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<InventoryItem> Products { get; set; }
        public int? StoreId { get; set; }
    }
}
