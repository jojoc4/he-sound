using HESound.Data;
using HESound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HESound.Controllers
{
    public class HomeController : Controller
    {
        private readonly HESoundContext _context;

        public HomeController(HESoundContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int personId, string searchString)
        {
            var sounds = from s in _context.Sounds select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sounds = sounds.Where(s => s.Title.Contains(searchString));
            }

            SelectList sl = null;
            if (personId == 0)
            {
                sl = new SelectList(_context.Persons, "PersonId", "Name");
            }
            else
            {
                sl = new SelectList(_context.Persons, "PersonId", "Name", personId);
                sounds = sounds.Where(s => s.PersonId == personId);
            }


            return View(new SoundPersonViewModel { 
                Sounds = await sounds.OrderBy(s => s.PersonId).ToListAsync(),
                People = sl,
                SearchString = searchString
            });
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
