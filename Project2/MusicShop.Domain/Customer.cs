using System.ComponentModel.DataAnnotations;

namespace MusicShop.Domain
{
    public class Customer
    {
        /// <summary>
        /// Gets or sets unique Primary key for the customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets of sets the first name of customer
        /// </summary>
        [StringLength(128)]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets of sets the last name of customer
        /// </summary>
        [StringLength(128)]
        public string LastName { get; set; }
        /// <summary>
        /// Gets of sets the email of customer
        /// </summary>
        [StringLength(128)]
        public string Email { get; set; }
        /// <summary>
        /// Gets of sets the phone number of customer
        /// </summary>
        [StringLength(128)]
        public string PhoneNo { get; set; }
        /// <summary>
        /// Gets of sets the password of customer
        /// </summary>
        [StringLength(128)]
        public string Password { get; set; }
        /// <summary>
        /// Gets of sets the customer Address of customer
        /// </summary>

        public CustomerAddress CustomerAddress { get; set; }
        /// <summary>
        /// Gets of sets the user type of customer, foreign key.
        /// </summary>
       
        public int UserTypeId { get; set; }

        /// <summary>
        /// Overrides the ToString method to display information about the Customer
        /// </summary>
        public override string ToString()
        {
            return $"Customer Details : \nId: {Id} \nFirst Name: {FirstName} \nLast Name: {LastName} \nEmail: {Email}";
        }
    }
}
