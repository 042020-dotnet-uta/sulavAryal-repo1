using System;
using System.Collections.Generic;
using System.Text;

namespace MusicShop.Domain
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
