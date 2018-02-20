using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektPracownia.Data;
using Microsoft.EntityFrameworkCore;
using ProjektPracownia.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ProjektPracownia.Controllers
{
    public class FaultSearchController : Controller
    {
        private readonly FaultsContext _context;
        public FaultSearchController(FaultsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var carMakes = await _context.CarMakes.ToListAsync();
            ViewBag.itemList = carMakes;
            ViewBag.titleText = "Search faults by car";

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (CarMake car in carMakes)
            {
                items.Add(new SelectListItem { Text = car.make, Value = car.CarMakeID.ToString() });
            }
            ViewBag.dropList = items;
            ViewBag.faultsText = "Known faults";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetModelsByMake(string carMake)
        {
            if (carMake != "0")
            {
                int numMake = 0;
                Int32.TryParse(carMake, out numMake);
                //var carModels = await _context.CarModels.Where(model => model.CarMakeID == numMake).ToListAsync();
                var models = await (from m in _context.CarModels
                                    where m.CarMakeID == numMake
                                    select new
                                    {
                                        carModelID = m.CarModelID,
                                        model = m.model
                                    }).ToListAsync();

                //var json = JsonConvert.SerializeObject(carModels);
                return Json(models);
            }
            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> GetVersionsByModel(string carModel)
        {
            if (carModel != "0")
            {
                int numModel = 0;
                Int32.TryParse(carModel, out numModel);
               // var carModels = await _context.CarVersion.Where(model => model.CarVersionID == numModel).ToListAsync();
               var versions = await (from m in _context.CarVersion
                                        where m.CarModelID == numModel
                                     select new
                                        {
                                            carVersionID = m.CarVersionID,
                                            version = m.version
                                        }).ToListAsync();

                return Json(versions);
            }
            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> GetVersionFaults(string carVersion)
        {
            if (carVersion != "0")
            {
                int numVersion = 0;
                Int32.TryParse(carVersion, out numVersion);
                var faults = await (from f in _context.CarFaults
                              join c in _context.FaultConnections
                              on f.CarFaultID equals c.CarFaultID
                              where c.CarVersionID == numVersion
                              select new
                              {
                                  faultID = f.CarFaultID,
                                  versionID = c.CarVersionID,
                                  faultName = f.name,
                                  faultSeverity = Enum.GetName(typeof(CarFaultSeverity), f.severity),
                                  faultCost = string.Format("{0:C}", f.fixCost)

                              }).ToListAsync();

                return Json(faults);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> GetFaultsForModel(string carModel)
        {
            if (carModel != "0")
            {
                int numModel = 0;
                Int32.TryParse(carModel, out numModel);
                var faults = await (from f in _context.CarFaults
                                    join c in _context.FaultConnections
                                    on f.CarFaultID equals c.CarFaultID
                                    join v in _context.CarVersion
                                    on c.CarVersionID equals v.CarVersionID
                                    where v.CarModelID == numModel
                                    select new
                                    {
                                        faultID = f.CarFaultID,
                                        versionID = c.CarVersionID,
                                        modelID = v.CarModelID,
                                        faultName = f.name,
                                        faultSeverity = Enum.GetName(typeof(CarFaultSeverity), f.severity),//((CarFaultSeverity)f.severity).ToString(),
                                        faultCost = string.Format("{0:C}", f.fixCost)

                                    }).ToListAsync();
                //var carFaults = await _context.CarFaults.Join(_context.FaultConnections,).Where(model => model.CarVersionID == numModel).ToListAsync();

                return Json(faults);
            }
            return Json("");
        }
    }
}