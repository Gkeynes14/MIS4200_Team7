using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class recognition
    {
        internal DateTime DateTime;

        [Key] public int recognitionID { get; set; }

        //link to user
        [Display(Name = "Employee Being Recognized")]
        [Required]
        public Guid profileID { get; set; }
        public virtual userProfile UserProfile { get; set; }

        //link to value
        [Display(Name = "Centric Value")]
        [Required]
        public int valueID { get; set; }
        public virtual value Value { get; set; }

        [Display(Name = "Additional Comments")]
        [Required]
        public string recognitionDescription { get; set; }

        public DateTime Now { get; set; }




    }
}