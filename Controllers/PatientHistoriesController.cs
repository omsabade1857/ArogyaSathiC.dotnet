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

    [Authorize(Roles = "Doctor, Admin")]

    public class PatientHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientHistories
        public async Task<IActionResult> Index()
        {
            return _context.PatientHistory != null ?
                        View(await _context.PatientHistory.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PatientHistory'  is null.");
           // return View();
        }

        public async Task<IActionResult> Index2()
        {
            return _context.PatientHistory != null ?
                        View(await _context.PatientHistory.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PatientHistory'  is null.");
            // return View();
        }

        // GET: PatientHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientHistory == null)
            {
                return NotFound();
            }

            var patientHistory = await _context.PatientHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null || _context.PatientHistory == null)
            {
                return NotFound();
            }

            var patientHistory = await _context.PatientHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }

        // GET: PatientHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create2()
        {
            return View();
        }

        // POST: PatientHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,visitDate,doctorName,symptoms,medicine,remark")] PatientHistory patientHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Id,Name,Gender,Age,visitDate,doctorName,symptoms,medicine,remark")] PatientHistory patientHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index2));
            }
            return View(patientHistory);
        }

        // GET: PatientHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientHistory == null)
            {
                return NotFound();
            }

            var patientHistory = await _context.PatientHistory.FindAsync(id);
            if (patientHistory == null)
            {
                return NotFound();
            }
            return View(patientHistory);
        }

        // POST: PatientHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,visitDate,doctorName,symptoms,medicine,remark")] PatientHistory patientHistory)
        {
            if (id != patientHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientHistoryExists(patientHistory.Id))
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
            return View(patientHistory);
        }

        // GET: PatientHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientHistory == null)
            {
                return NotFound();
            }

            var patientHistory = await _context.PatientHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }

        // POST: PatientHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientHistory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientHistory'  is null.");
            }
            var patientHistory = await _context.PatientHistory.FindAsync(id);
            if (patientHistory != null)
            {
                _context.PatientHistory.Remove(patientHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientHistoryExists(int id)
        {
          return (_context.PatientHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
