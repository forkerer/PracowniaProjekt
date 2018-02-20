using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektPracownia.Data;
using ProjektPracownia.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ProjektPracownia.Controllers
{
    public class CarEditController : Controller
    {
        private readonly FaultsContext _context;
        public CarEditController(FaultsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var carMakes = await(from m in _context.CarMakes
                                 select m).ToListAsync();
            ViewBag.carMakes = carMakes;
            ViewBag.makesText = "Edit car makes";
                
            return View("index");
        }
    }
}