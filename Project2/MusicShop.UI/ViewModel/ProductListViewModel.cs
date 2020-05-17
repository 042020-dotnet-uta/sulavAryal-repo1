using MusicShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.ViewModel
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            this.Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
        public string CurrentStore { get; set; }
        public string CurrentCustomer { get; set; }
    }
}
