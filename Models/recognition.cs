using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class recognition
    {

        [Key] public int recognitionID { get; set; }

        //person recognizing
        [Display(Name = "Recognizer")]
        public Guid recognizerID { get; set; }
        
        
        //person being recognized
        [Display(Name = "Employee Being Recognized")]
        [Required]
        public Guid profileID { get; set; }
        
        public virtual userProfile UserProfile { get; set; }

        //extra comments
        [Display(Name = "Additional Comments")]
        [Required]
        public string recognitionDescription { get; set; }

        //timestamp of the recognition
        [Display(Name = "Recognition Date")]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy hh:mm}", ApplyFormatInEditMode =true)]
        public DateTime Now { get; set; }

        //centric value for recognition
        [Display(Name = "Centric Value")]
        [Required]
        public CoreValue award { get; set; }

        public enum CoreValue
        {
            Excellence = 1,
            Culture = 2,
            Integrity = 3,
            Stewardship = 4,
            Innovate = 5,
            Passion = 6,
            Balance = 7
        }

    }
}