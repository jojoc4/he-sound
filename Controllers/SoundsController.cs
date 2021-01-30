using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HESound.Data;
using HESound.Models;
using System.Net.Mime;
using HESound.Utilities;

namespace HESound.Controllers
{
    public class SoundsController : Controller
    {
        private readonly HESoundContext _context;

        public SoundsController(HESoundContext context)
        {
            _context = context;
        }

        // GET: Sounds
        public async Task<IActionResult> Index()
        {
            var hESoundContext = _context.Sounds.Include(s => s.Person).OrderBy(s => s.Person.Name);
            return View(await hESoundContext.ToListAsync());
        }
        public async Task<IActionResult> File(int id)
        {
            var requestFile = await _context.Sounds.SingleOrDefaultAsync(m => m.SoundId == id);
            return File(requestFile.Content, MediaTypeNames.Application.Octet);
        }

        // GET: Sounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sound = await _context.Sounds
                .Include(s => s.Person)
                .FirstOrDefaultAsync(m => m.SoundId == id);
            if (sound == null)
            {
                return NotFound();
            }

            return View(sound);
        }

        // GET: Sounds/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "Name");
            return View();
        }

        // POST: Sounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoundId,Title,PersonId,FormFile")] SoundUploadViewModel soundUploadViewModel)
        {
            Sound sound = new Sound();
            if (ModelState.IsValid)
            {
                sound.PersonId = soundUploadViewModel.PersonId;
                sound.Title = soundUploadViewModel.Title;
                String[] ext = { ".mp3", ".wav"};
                sound.Content = await FileHelpers.ProcessFormFile<SoundUploadViewModel>(soundUploadViewModel.FormFile, ModelState, ext, 2097152);
                _context.Add(sound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "Name", soundUploadViewModel.PersonId);
            return View(soundUploadViewModel);
        }

        // GET: Sounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sound = await _context.Sounds.FindAsync(id);
            if (sound == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "Name", sound.PersonId);
            return View(sound);
        }

        // POST: Sounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoundId,Title,PersonId")] Sound sound)
        {
            if (id != sound.SoundId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoundExists(sound.SoundId))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "Name", sound.PersonId);
            return View(sound);
        }

        // GET: Sounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sound = await _context.Sounds
                .Include(s => s.Person)
                .FirstOrDefaultAsync(m => m.SoundId == id);
            if (sound == null)
            {
                return NotFound();
            }

            return View(sound);
        }

        // POST: Sounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sound = await _context.Sounds.FindAsync(id);
            _context.Sounds.Remove(sound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoundExists(int id)
        {
            return _context.Sounds.Any(e => e.SoundId == id);
        }
    }
}
