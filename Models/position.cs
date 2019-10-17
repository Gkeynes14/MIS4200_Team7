using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class position
    {
        [Key]public int positionID { get; set; }

        [Display (Name ="Position Title")]
        [Required]
        public string positionTitle { get; set; }

        [Display(Name = "Position Description")]
        [Required]
        public string positionDescription { get; set; }



        //link to userProfile
        public ICollection<userProfile> userProfiles { get; set; }


    }
}