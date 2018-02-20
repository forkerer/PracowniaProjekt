using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektPracownia.Models
{
    public class FaultConnection
    {
        public int FaultConnectionID { get; set; }

        public FaultConnection(int fault, int version)
        {
            CarFaultID = fault;
            CarVersionID = version;
        }

        public int CarFaultID { get; set; }
        public CarFault CarFault{ get; set; }

        public int CarVersionID { get; set; }
        public CarVersion CarVersion { get; set; }
    }
}
