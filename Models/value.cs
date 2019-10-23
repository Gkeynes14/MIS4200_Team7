using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team7.Models
{
    public class value
    {
        [Key]
        public int valueID { get; set; }

        [Display(Name = "Value Name")]
        [Required]
        public string valueName { get; set; }

        [Display(Name = "Value Description")]
        [Required]
        public string valueDescription { get; set; }


        //link to recognition

        public ICollection<recognition> recognitions { get; set; }

    }
}