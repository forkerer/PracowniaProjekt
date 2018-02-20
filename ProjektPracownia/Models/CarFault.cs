using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektPracownia.Models
{
    public enum CarFaultSeverity
    {
        SERIOUS, MEDIUM, TRIVIAL
    }
    public class CarFault
    {
        public int CarFaultID { get; set; }

        [Required]
        public string name { get; set; }

        public CarFaultSeverity? severity { get; set; }

        [DataType(DataType.Currency)]
        public int fixCost { get; set; }

        public ICollection<FaultConnection> faultsConnections { get; set; }
    }
}