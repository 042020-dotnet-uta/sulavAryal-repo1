using MusicShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.ViewModel
{
    public class OrderDetailViewModel
    {
        /// <summary>
        /// Gets and sets order object
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// Gets and sets IEnumerable of type product
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
    }
}
