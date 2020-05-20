using MusicShop.Domain;
using System.Collections.Generic;

namespace MusicShop.UI.ViewModel
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            this.Products = new List<Product>();
        }
        /// <summary>
        /// Gets, Sets IEnumerable of Products
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// Gets, Sets CurrentStore
        /// </summary>
        public string CurrentStore { get; set; }
        /// <summary>
        /// Gets, Sets CurrentCustomer
        /// </summary>
        public string CurrentCustomer { get; set; }
    }
}
