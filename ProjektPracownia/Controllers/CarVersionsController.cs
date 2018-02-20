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
    public class CarVersionsController : Controller
    {
        private readonly FaultsContext _context;

        public CarVersionsController(FaultsContext context)
        {
            _context = context;
        }

        // GET: CarVersions
        public async Task<IActionResult> Index()
        {
            var faultsContext = _context.CarVersion.Include(c => c.carModel);
            return View(await faultsContext.ToListAsync());
        }

        // GET: CarVersions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carVersion = await _context.CarVersion
                .Include(c => c.carModel)
                .SingleOrDefaultAsync(m => m.CarVersionID == id);
            if (carVersion == null)
            {
                return NotFound();
            }

            return View(carVersion);
        }

        // GET: CarVersions/Create
        public IActionResult Create(int id)
        {
            var list = (from m in _context.CarModels
                        where m.CarModelID == id
                        select m).ToList();
            ViewData["CarModelID"] = new SelectList(list, "CarModelID", "model", id);
            return View();
        }

        // POST: CarVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarVersionID,version,CarModelID")] CarVersion carVersion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carVersion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelID"] = new SelectList(_context.CarModels, "CarModelID", "model", carVersion.CarModelID);
            return View(carVersion);
        }

        // GET: CarVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carVersion = await _context.CarVersion.SingleOrDefaultAsync(m => m.CarVersionID == id);
            if (carVersion == null)
            {
                return NotFound();
            }
            var list = await (from m in _context.CarModels
                        join c in _context.CarVersion
                        on m.CarModelID equals c.CarModelID
                        where c.CarVersionID == id
                        select m).ToListAsync();

            ViewData["CarModelID"] = new SelectList(list, "CarModelID", "model", carVersion.CarModelID);
            return View(carVersion);
        }

        // POST: CarVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarVersionID,version,CarModelID")] CarVersion carVersion)
        {
            if (id != carVersion.CarVersionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carVersion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarVersionExists(carVersion.CarVersionID))
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
            ViewData["CarModelID"] = new SelectList(_context.CarModels, "CarModelID", "model", carVersion.CarModelID);
            return View(carVersion);
        }

        // GET: CarVersions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carVersion = await _context.CarVersion
                .Include(c => c.carModel)
                .SingleOrDefaultAsync(m => m.CarVersionID == id);
            if (carVersion == null)
            {
                return NotFound();
            }

            return View(carVersion);
        }

        // POST: CarVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carVersion = await _context.CarVersion.SingleOrDefaultAsync(m => m.CarVersionID == id);
            _context.CarVersion.Remove(carVersion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarVersionExists(int id)
        {
            return _context.CarVersion.Any(e => e.CarVersionID == id);
        }
    }
}
