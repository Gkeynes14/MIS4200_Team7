using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class userProfile
    {
        [Required]
        [Key]
        public Guid profileID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        public string fullName { get { return lastName + ", " + firstName; } }

        [Required]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public string hireDate { get; set; }

        //link to position
        [Display(Name = "Current position")]
        [Required]
        public int positionID { get; set; }
        public virtual position Position { get; set; }

    }
}