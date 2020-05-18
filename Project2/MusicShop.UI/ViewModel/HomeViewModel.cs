using MusicShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<InventoryItem> Products { get; set; }
        public int? StoreId { get; set; }
    }
}
