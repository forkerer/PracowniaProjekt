using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektPracownia.Models;
using ProjektPracownia.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProjektPracownia.Controllers
{
    public class CarSearchController : Controller
    {
        private readonly FaultsContext _context;
        public CarSearchController(FaultsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var faults = await _context.CarFaults.ToListAsync();
            ViewBag.faultsList = faults;
            ViewBag.faultsText = "Cars with issue";
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAutocompleteData()
        {

            var faults = await (from f in _context.CarFaults
                                select new
                                {
                                    id = f.CarFaultID.ToString(),
                                    name = f.name,
                                }).ToListAsync();
            //var carFaults = await _context.CarFaults.Join(_context.FaultConnections,).Where(model => model.CarVersionID == numModel).ToListAsync();

            return Json(faults);
        }

        [HttpPost]
        public async Task<IActionResult> GetCarsByFaultID(string faultID)
        {

            {
                if (faultID != "0")
                {
                    int numFault = 0;
                    if (Int32.TryParse(faultID, out numFault))
                    {
                        var faults = await (from f in _context.CarFaults
                                            join c in _context.FaultConnections
                                            on f.CarFaultID equals c.CarFaultID
                                            join v in _context.CarVersion
                                            on c.CarVersionID equals v.CarVersionID
                                            join m in _context.CarModels
                                            on v.CarModelID equals m.CarModelID
                                            join make in _context.CarMakes
                                            on m.CarMakeID equals make.CarMakeID
                                            where f.CarFaultID == numFault
                                            orderby make.make, m.model ascending
                                            select new
                                            {
                                                faultID = f.CarFaultID,
                                                versionID = c.CarVersionID,
                                                modelID = v.CarModelID,
                                                makeID = make.CarMakeID,
                                                faultName = f.name,
                                                faultSeverity = Enum.GetName(typeof(CarFaultSeverity), f.severity),//((CarFaultSeverity)f.severity).ToString(),
                                                faultCost = string.Format("{0:C}", f.fixCost),
                                                carMake = make.make,
                                                carModel = m.model,
                                                CarVersion = v.version

                                            }).ToListAsync();

                        return Json(faults);
                    }
                    return Json("");
                }
                return Json("");
            }
        }
    }
}