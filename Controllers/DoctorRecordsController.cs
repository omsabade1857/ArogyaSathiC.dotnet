using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AarogyaSaathi.Data;
using AarogyaSaathi.Models;
using Microsoft.AspNetCore.Authorization;

namespace AarogyaSaathi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DoctorRecords
        public async Task<IActionResult> Index()
        {
              return _context.DoctorData != null ? 
                          View(await _context.DoctorData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DoctorData'  is null.");
        }

        // GET: DoctorRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorData == null)
            {
                return NotFound();
            }

            var doctor = await _context.DoctorData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: DoctorRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DoctorRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MobileNo,Qualification,Specialization")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: DoctorRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorData == null)
            {
                return NotFound();
            }

            var doctor = await _context.DoctorData.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: DoctorRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MobileNo,Qualification,Specialization")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }

        // GET: DoctorRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorData == null)
            {
                return NotFound();
            }

            var doctor = await _context.DoctorData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: DoctorRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DoctorData'  is null.");
            }
            var doctor = await _context.DoctorData.FindAsync(id);
            if (doctor != null)
            {
                _context.DoctorData.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.DoctorData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
