using System;
using System.Collections.Generic;
using System.Text;

namespace MusicShop.Domain
{
    public class UserType
    {
        /// <summary>
        /// Gets or sets Primary key of UserType
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets type (admin vs customer) (1 vs 2) of UserType
        /// </summary>
        public string Type { get; set; }
    }
}
