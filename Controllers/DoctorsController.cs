using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AarogyaSaathi.Data;
using AarogyaSaathi.Models;
using System.Security.Claims;
using AarogyaSaathi.Dto;
using Microsoft.AspNetCore.Authorization;

namespace AarogyaSaathi.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
              return _context.AppointmentData != null ? 
                          View(await _context.AppointmentData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AppointmentData'  is null.");
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppointmentData == null)
            {
                return NotFound();
            }

            var appointment = await _context.AppointmentData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoctorId,Symptoms,BookingDate,Status,PatientId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppointmentData == null)
            {
                return NotFound();
            }

            var appointment = await _context.AppointmentData.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoctorId,Symptoms,BookingDate,Status,PatientId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.AppointmentData.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ShowApp");
            }
            return RedirectToAction("ShowApp");
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppointmentData == null)
            {
                return NotFound();
            }

            var appointment = await _context.AppointmentData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppointmentData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppointmentData'  is null.");
            }
            var appointment = await _context.AppointmentData.FindAsync(id);
            if (appointment != null)
            {
                _context.AppointmentData.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowApp");
        }

        private bool AppointmentExists(int id)
        {
          return (_context.AppointmentData?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult ShowApp() {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appointments = _context.AppointmentData.Where(a => a.DoctorId == userId).ToList();

            return View(appointments);
        }


        [HttpGet]
        public async Task<IActionResult> EditDApp(int? id)
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
        public async Task<IActionResult> EditDApp(int id, Appointment app)
        {
            Appointment currApp = _context.AppointmentData.Find(id);
            //currApp.Symptoms = booking.Symptoms;
            //currApp.BookingDate = booking.BookingDate;
            currApp.Status = app.Status;
            _context.Update(currApp);
            await _context.SaveChangesAsync();

            return RedirectToAction("ShowApp");
        }
    }
}
