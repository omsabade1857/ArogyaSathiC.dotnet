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
using Microsoft.AspNetCore.Identity;
using AarogyaSaathi.Dto;
using System.Security.Claims;

namespace AarogyaSaathi.Controllers
{
    [Authorize(Roles ="Patient")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return _context.PatientData != null ?
                        View(await _context.PatientData.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PatientData'  is null.");
        }

        // GET: Patients/Details/5
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

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,DateOfBirth,City,MobileNo,email,Password")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
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

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,DateOfBirth,City,MobileNo,email,Password")] Patient patient)
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

        // GET: Patients/Delete/5
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

        // POST: Patients/Delete/5
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

        [HttpGet]
        public IActionResult BookApp() {
            return View();
        }

        [HttpPost]
        public IActionResult BookApp(BookingView booking)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newApp = new Appointment();

            newApp.DoctorId = booking.DoctorId;
            newApp.Symptoms = booking.Symptoms;
            newApp.BookingDate = booking.BookingDate;
            newApp.Status = "pending";
            newApp.PatientId = userId;
            _context.AppointmentData.Add(newApp);
            _context.SaveChanges();

            return RedirectToAction("ShowApp");
        }

        [HttpGet]
        public IActionResult ShowApp() {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appointments= _context.AppointmentData.Where(a => a.PatientId == userId).ToList();
           
            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> EditApp(int? id)
        {
            if (id == null || _context.AppointmentData == null)
            {
                return NotFound();
            }

            var currentApp = await _context.AppointmentData.FindAsync(id);
            if (currentApp == null)
            {
                return NotFound();
            }
            return View(currentApp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApp(int id, BookingView booking)
        {
                Appointment currApp = _context.AppointmentData.Find(id);
                currApp.Symptoms = booking.Symptoms;
                currApp.BookingDate = booking.BookingDate;
                _context.Update(currApp);
                 await _context.SaveChangesAsync();
                    
                return RedirectToAction("ShowApp");
        }
       
        public async Task<IActionResult> DeleteApp(int? id)
        {
            if (id == null || _context.AppointmentData == null)
            {
                return NotFound();
            }

            var currApp = await _context.AppointmentData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currApp == null)
            {
                return NotFound();
            }

            return View(currApp);
        }

        [HttpPost, ActionName("DeleteApp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAppConfirmed(int id)
        {
            if (_context.AppointmentData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppointmentData'  is null.");
            }
            var currApp = await _context.AppointmentData.FindAsync(id);
            if (currApp != null)
            {
                _context.AppointmentData.Remove(currApp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ShowApp");
        }
    }
}
