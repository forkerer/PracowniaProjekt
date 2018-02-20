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
    public class CarModelsController : Controller
    {
        private readonly FaultsContext _context;

        public CarModelsController(FaultsContext context)
        {
            _context = context;
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            var faultsContext = _context.CarModels.Include(c => c.CarMake);
            return View(await faultsContext.ToListAsync());
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.CarMake)
                .SingleOrDefaultAsync(m => m.CarModelID == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModels/Create
        public IActionResult Create(int id)
        {
            var selectionList = new SelectList(_context.CarMakes, "CarMakeID", "make", id);

            ViewData["CarMakeID"] = selectionList;
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarModelID,model,CarMakeID")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("../CarEdit/Index");
            }
            ViewData["CarMakeID"] = new SelectList(_context.CarMakes, "CarMakeID", "make", carModel.CarMakeID);
            return View("../CarEdit/Index");
        }

        // GET: CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.SingleOrDefaultAsync(m => m.CarModelID == id);
            if (carModel == null)
            {
                return NotFound();
            }
            ViewData["CarMakeID"] = new SelectList(_context.CarMakes, "CarMakeID", "make", carModel.CarMakeID);
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarModelID,model,CarMakeID")] CarModel carModel)
        {
            if (id != carModel.CarModelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.CarModelID))
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
            ViewData["CarMakeID"] = new SelectList(_context.CarMakes, "CarMakeID", "make", carModel.CarMakeID);
            return View("../CarEdit/Index");
        }

        // GET: CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.CarMake)
                .SingleOrDefaultAsync(m => m.CarModelID == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModels.SingleOrDefaultAsync(m => m.CarModelID == id);
            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("../CarEdit/Index");
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.CarModelID == id);
        }
    }
}
