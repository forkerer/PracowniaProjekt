using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjektPracownia.Models
{
    public class CarVersion
    {
        [DataMember]
        public int CarVersionID { get; set; }

        [Required]
        [DataMember]
        public string version { get; set; }

        public int CarModelID { get; set; }
        public CarModel carModel { get; set; }

        public ICollection<CarFault> Faults { get; set; }
    }
}
