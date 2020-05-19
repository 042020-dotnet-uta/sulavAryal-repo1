using MusicShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.ViewModel
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
