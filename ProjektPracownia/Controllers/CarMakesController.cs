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
    public class CarMakesController : Controller
    {
        private readonly FaultsContext _context;

        public CarMakesController(FaultsContext context)
        {
            _context = context;
        }

        // GET: CarMakes
        public async Task<IActionResult> Index()
        {
            var carMakes = await _context.CarMakes.ToListAsync();
            ViewBag.carMakes = carMakes;
            ViewBag.makesText = "Edit car makes";

            return RedirectToAction("Index", "CarEdit"); ;
        }

        // GET: CarMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes
                .SingleOrDefaultAsync(m => m.CarMakeID == id);
            if (carMake == null)
            {
                return NotFound();
            }

            return View(carMake);
        }

        // GET: CarMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarMake carMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carMake);
                await _context.SaveChangesAsync();
                return RedirectToAction("../CarEdit/Index");
            }
            return View("../CarEdit/Index");
        }

        // GET: CarMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes.SingleOrDefaultAsync(m => m.CarMakeID == id);
            if (carMake == null)
            {
                return NotFound();
            }
            return View(carMake);
        }

        // POST: CarMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarMakeID,make")] CarMake carMake)
        {
            if (id != carMake.CarMakeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMakeExists(carMake.CarMakeID))
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
            return View(carMake);
        }

        // GET: CarMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes
                .SingleOrDefaultAsync(m => m.CarMakeID == id);
            if (carMake == null)
            {
                return NotFound();
            }

            return View(carMake);
        }

        // POST: CarMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carMake = await _context.CarMakes.SingleOrDefaultAsync(m => m.CarMakeID == id);
            _context.CarMakes.Remove(carMake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMakeExists(int id)
        {
            return _context.CarMakes.Any(e => e.CarMakeID == id);
        }
    }
}
