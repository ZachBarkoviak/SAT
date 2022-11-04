﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SAT.DATA.EF.Models;

namespace SAT.UI.MVC.Controllers
{
    
    public class EnrollmentsController : Controller
    {
        private readonly SATContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EnrollmentsController(SATContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Enrollments
        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Student"))
            {
                var sATContext = _context.Enrollments.Include(e => e.ScheduledClass).Include(e => e.Student).Include(e => e.ScheduledClass.Course).Where(e => e.Student.Email == User.Identity.Name);
                return View(await sATContext.ToListAsync());
            }
            else 
            { 
                var sATContext = _context.Enrollments.Include(e => e.ScheduledClass).Include(e => e.Student).Include(e => e.ScheduledClass.Course);            
                return View(await sATContext.ToListAsync());
            }
        }

        // GET: Enrollments/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.ScheduledClass)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        [Authorize(Roles = "Admin, Scheduling")]
        public IActionResult Create()
        {
            ViewData["ScheduledClassId"] = new SelectList(_context.ScheduledClasses, "ScheduledClassId", "ScheduledClassId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Scheduling")]
        public async Task<IActionResult> Create([Bind("EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduledClassId"] = new SelectList(_context.ScheduledClasses, "ScheduledClassId", "InscructorName", enrollment.ScheduledClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        [Authorize(Roles = "Admin, Scheduling")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["ScheduledClassId"] = new SelectList(_context.ScheduledClasses, "ScheduledClassId", "InscructorName", enrollment.ScheduledClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Scheduling")]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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
            ViewData["ScheduledClassId"] = new SelectList(_context.ScheduledClasses, "ScheduledClassId", "InscructorName", enrollment.ScheduledClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        [Authorize(Roles = "Admin, Scheduling")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.ScheduledClass)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Scheduling")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'SATContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
