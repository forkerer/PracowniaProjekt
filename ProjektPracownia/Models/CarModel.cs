using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjektPracownia.Models
{
    public class CarModel
    {
        [DataMember]
        public int CarModelID { get; set; }

        [Required]
        [DataMember]
        public string model { get; set; }

        public int CarMakeID { get; set; }
        public CarMake CarMake { get; set; }

        public ICollection<CarVersion> Versions { get; set; }
    }
}