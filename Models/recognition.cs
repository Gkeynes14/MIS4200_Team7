using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS4200_Team7.Models
{
    public class recognition
    {
        [Key] public int recognitionID { get; set; }
        public string recognitionMessage { get; set; }

        public static DateTime datePosted { get; set; }

        //link userProfile
        public ICollection<userProfile> userProfile { get; set; }
    }
}