using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicShop.Domain
{
    public class CustomerAddress
    {
        /// <summary>
        /// Gets or sets the primary key of CustomerAddress
        /// </summary>
        public int Id { get; set; }
        [StringLength(128)]
        [Required]
        /// <summary>
        /// Gets or sets the Street name of customer
        /// </summary>

        public string Street { get; set; }
        [StringLength(128)]
        [Required]
        /// <summary>
        /// Gets or sets name of city for the customer
        /// </summary>
        public string City { get; set; }
        [StringLength(128)]
        [Required]
        public string  State { get; set; }
        /// <summary>
        /// Gets or sets the zipcode of customer
        /// </summary>
        [StringLength(128)]
        [Required]
        public string Zip { get; set; }
        /// <summary>
        /// Gets or sets the customer Id of customer, this is a foreign key
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets Navigational property for the Customer
        /// </summary>
        public Customer Customer { get; set; }

    }
}
