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
    public class PatientRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientRecords
        public async Task<IActionResult> Index()
        {
              return _context.PatientData != null ? 
                          View(await _context.PatientData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PatientData'  is null.");
        }

        // GET: PatientRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientData == null)
            {
                return NotFound();
            }

            var patient = await _context.PatientData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: PatientRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,DateOfBirth,City,MobileNo")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: PatientRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientData == null)
            {
                return NotFound();
            }

            var patient = await _context.PatientData.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: PatientRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,DateOfBirth,City,MobileNo")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: PatientRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientData == null)
            {
                return NotFound();
            }

            var patient = await _context.PatientData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: PatientRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientData'  is null.");
            }
            var patient = await _context.PatientData.FindAsync(id);
            if (patient != null)
            {
                _context.PatientData.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return (_context.PatientData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
