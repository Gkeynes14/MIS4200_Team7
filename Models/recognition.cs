using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class recognition
    {

        [Key] public int recognitionID { get; set; }


        [Display(Name = "Recognizer")]
        public string recognizerID { get; set; }

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

        [Display(Name = "Recognition Date")]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy hh:mm}", ApplyFormatInEditMode =true)]
        public DateTime Now { get; set; }

    }
}