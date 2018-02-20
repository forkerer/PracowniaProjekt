using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjektPracownia.Models
{
    public class CarMake
    {   
        [DataMember]
        public int CarMakeID { get; set; }

        [Required]
        [Display(Name = "Car make")]
        [DataMember]
        public string make { get; set; }

        public ICollection<CarModel> Models { get; set; }
    }
}