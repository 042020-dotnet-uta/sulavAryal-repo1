using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcApp.Domain
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
