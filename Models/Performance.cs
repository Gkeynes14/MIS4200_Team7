using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MIS4200_Team7.Models
{
    public class Performance
    {
        public int performanceID { get; set; }
        public string ratingDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateRated { get; set; }
    }
}