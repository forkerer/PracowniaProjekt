using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektPracownia.Data;
using ProjektPracownia.Models;

namespace ProjektPracownia.Controllers
{
    public class CarFaultsController : Controller
    {
        private readonly FaultsContext _context;

        public CarFaultsController(FaultsContext context)
        {
            _context = context;
        }

        // GET: CarFaults
        public async Task<IActionResult> Index()
        {
            ViewBag.carFaults = await _context.CarFaults.ToListAsync();
            return View();
        }

        // GET: CarFaults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carFault = await _context.CarFaults
                .SingleOrDefaultAsync(m => m.CarFaultID == id);
            if (carFault == null)
            {
                return NotFound();
            }

            return View(carFault);
        }

        // GET: CarFaults/Create
        public IActionResult Create()
        {
            var EntityState = new SelectList(Enum.GetValues(typeof(CarFaultSeverity)).Cast<CarFaultSeverity>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            ViewData["carFaultsSeverity"] = EntityState;
            return View();
        }

        // POST: CarFaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarFaultID,name,severity,fixCost")] CarFault carFault)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carFault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carFault);
        }

        // GET: CarFaults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carFault = await _context.CarFaults.SingleOrDefaultAsync(m => m.CarFaultID == id);
            if (carFault == null)
            {
                return NotFound();
            }
            var EntityState = new SelectList(Enum.GetValues(typeof(CarFaultSeverity)).Cast<CarFaultSeverity>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            ViewData["carFaultsSeverity"] = EntityState;

            return View(carFault);
        }

        // POST: CarFaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarFaultID,name,severity,fixCost")] CarFault carFault)
        {
            if (id != carFault.CarFaultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carFault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarFaultExists(carFault.CarFaultID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carFault);
        }

        // GET: CarFaults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carFault = await _context.CarFaults
                .SingleOrDefaultAsync(m => m.CarFaultID == id);
            if (carFault == null)
            {
                return NotFound();
            }

            return View(carFault);
        }

        // POST: CarFaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carFault = await _context.CarFaults.SingleOrDefaultAsync(m => m.CarFaultID == id);
            _context.CarFaults.Remove(carFault);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarFaultExists(int id)
        {
            return _context.CarFaults.Any(e => e.CarFaultID == id);
        }

        public async Task<IActionResult> ConnectTo(int id)
        {

            ViewBag.versionID = id;
            var versionName = await (from v in _context.CarVersion
                                   where v.CarVersionID == id
                                   select v.version).FirstOrDefaultAsync();
            if (versionName != null)
            {
                ViewBag.versionName = versionName;
                ViewBag.faultsList = await _context.CarFaults.ToListAsync();
                return View();
            }

            return NotFound();
        }

        public async Task<IActionResult> ConnectFault(int faultID, int versionID)
        {
            _context.FaultConnections.Add(new FaultConnection(faultID, versionID));
            await _context.SaveChangesAsync();
            return Json("");
        }

        public async Task<IActionResult> RemoveConnection(int id, int? versionID)
        {
            if ((versionID == null) || (id <= 0))
            {
                return NotFound();
            }
            ViewBag.versionID = versionID;
            ViewBag.faultID = id;
            var versionName = await (from v in _context.CarVersion
                                     where v.CarVersionID == versionID
                                     select v.version).FirstOrDefaultAsync();
            var faultName = await (from v in _context.CarFaults
                                   where v.CarFaultID == id
                                   select v.name).FirstOrDefaultAsync();
            if ((versionName != null) && (faultName != null))
            {
                ViewBag.versionName = versionName;
                ViewBag.faultName = faultName;
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DisconnectFault(int id, int versionID)
        {
            //_context.FaultConnections.Add(new FaultConnection(faultID, versionID));
            var conn = (from c in _context.FaultConnections
                        //where c.CarFaultID == id && c.CarVersionID == versionID
                        select c).ToList();
            if (conn != null)
            {
                //_context.FaultConnections.Remove(conn);
                await _context.SaveChangesAsync();
                return Json("");
            }
            return NotFound();
        }
    }
}
