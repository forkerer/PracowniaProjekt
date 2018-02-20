using ProjektPracownia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektPracownia.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FaultsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.CarMakes.Any())
            {
                return;   // DB has been seeded
            }

            var CarMakes = new CarMake[]
            {
            new CarMake{make="Audi"},
            new CarMake{make="Peugeot"},
            new CarMake{make="BMW"},
            new CarMake{make="Porsche"},
            new CarMake{make="Toyota"},
            new CarMake{make="Hyundai"},
            new CarMake{make="Fiat"},
            };
            foreach (CarMake s in CarMakes)
            {
                context.CarMakes.Add(s);
            }
            context.SaveChanges();

            var CarModels = new CarModel[]
           {
            new CarModel{CarMakeID=1,model="A4"},
            new CarModel{CarMakeID=1,model="A5"},
            new CarModel{CarMakeID=1,model="A7"},
            new CarModel{CarMakeID=1,model="S4"},
            new CarModel{CarMakeID=1,model="RS4"},
            new CarModel{CarMakeID=2,model="307"},
            new CarModel{CarMakeID=2,model="207"},
            new CarModel{CarMakeID=2,model="3008"},
            new CarModel{CarMakeID=3,model="M3"},
            new CarModel{CarMakeID=3,model="E86"},
            new CarModel{CarMakeID=4,model="Boxter"},
            new CarModel{CarMakeID=4,model="Cayenne"},
            new CarModel{CarMakeID=4,model="Carrera 911"},
            new CarModel{CarMakeID=7,model="500"},
            new CarModel{CarMakeID=7,model="126p"}
           };
            foreach (CarModel s in CarModels)
            {
                context.CarModels.Add(s);
            }
            context.SaveChanges();

            var CarVersions = new CarVersion[]
            {
            new CarVersion{CarModelID=1, version="Standard"},
            new CarVersion{CarModelID=1, version="Executive"},
            new CarVersion{CarModelID=2, version="Standard"},
            new CarVersion{CarModelID=3, version="Standard"},
            new CarVersion{CarModelID=4, version="Standard"},
            new CarVersion{CarModelID=5, version="Standard"},
            new CarVersion{CarModelID=6, version="Standard"},
            new CarVersion{CarModelID=7, version="Standard"},
            new CarVersion{CarModelID=8, version="Standard"},
            new CarVersion{CarModelID=9, version="Standard"},
            new CarVersion{CarModelID=10, version="Standard"},
            new CarVersion{CarModelID=11, version="Standard"},
            new CarVersion{CarModelID=12, version="Standard"},
            new CarVersion{CarModelID=13, version="Standard"},
            new CarVersion{CarModelID=14, version="Standard"},

            };
            foreach (CarVersion c in CarVersions)
            {
                context.CarVersion.Add(c);
            }
            context.SaveChanges();

            var Faults = new CarFault[]
            {
            new CarFault{name="AC Blowing up",fixCost=2000, severity=CarFaultSeverity.SERIOUS},
            new CarFault{name="Coolant leaking",fixCost=50, severity=CarFaultSeverity.MEDIUM},
            new CarFault{name="Car blows up",fixCost=20000, severity=CarFaultSeverity.SERIOUS},
            new CarFault{name="Cup holder broken",fixCost=10, severity=CarFaultSeverity.TRIVIAL},
            };
            foreach (CarFault f in Faults)
            {
                context.CarFaults.Add(f);
            }
            context.SaveChanges();

            var FaultConnections = new FaultConnection[]
            {
            new FaultConnection(1,1),
            new FaultConnection(2,1),
            new FaultConnection(3,1),
            new FaultConnection(4,1),
            new FaultConnection(2,3),
            };
            foreach (FaultConnection f in FaultConnections)
            {
                context.FaultConnections.Add(f);
            }
            context.SaveChanges();
        }
    }
}
