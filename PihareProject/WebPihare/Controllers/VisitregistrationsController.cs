using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPihare.Context;
using WebPihare.Entities;
using WebPihare.Models;

namespace WebPihare.Controllers
{
    [Authorize]
    public class VisitregistrationsController : Controller
    {
        private readonly PihareiiContext _context;

        public VisitregistrationsController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Visitregistrations
        public async Task<IActionResult> Index()
        {
            var pihareiiContext = _context.Visitregistration.Include(v => v.Client).Include(v => v.Commisioner).Include(v => v.Department);
            return View(await pihareiiContext.ToListAsync());
        }

        // GET: Visitregistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitregistration = await _context.Visitregistration
                .Include(v => v.Client)
                .Include(v => v.Commisioner)
                .Include(v => v.Department)
                .FirstOrDefaultAsync(m => m.VisitRegistrationId == id);
            if (visitregistration == null)
            {
                return NotFound();
            }

            return View(visitregistration);
        }

        // GET: Visitregistrations/Create
        public async Task<IActionResult> Create(int? id)
        {
            RegisterViewModel model = new RegisterViewModel();

            var idUser = int.Parse( User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

            model.Department = await _context.Department.Include(v => v.DepartmentState).Include(v => v.DepartmentType).FirstOrDefaultAsync(m => m.DepartmentId == id);
            model.Commisioner = await _context.Commisioner.Include(v => v.Role).FirstOrDefaultAsync(m => m.CommisionerId == idUser);

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName");
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "CommisionerPassword");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitRegistrationId,ReferencialPrice,ClientRegister,VisitDay,Observations,ClientId,DepartmentId,CommisionerId")] Visitregistration visitregistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitregistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", visitregistration.ClientId);
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "CommisionerPassword", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentDescription", visitregistration.DepartmentId);
            return View(visitregistration);
        }

        // GET: Visitregistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitregistration = await _context.Visitregistration.FindAsync(id);
            if (visitregistration == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", visitregistration.ClientId);
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "CommisionerPassword", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentDescription", visitregistration.DepartmentId);
            return View(visitregistration);
        }

        // POST: Visitregistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitRegistrationId,ReferencialPrice,ClientRegister,VisitDay,Observations,ClientId,DepartmentId,CommisionerId")] Visitregistration visitregistration)
        {
            if (id != visitregistration.VisitRegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitregistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitregistrationExists(visitregistration.VisitRegistrationId))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", visitregistration.ClientId);
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "CommisionerPassword", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentDescription", visitregistration.DepartmentId);
            return View(visitregistration);
        }

        // GET: Visitregistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitregistration = await _context.Visitregistration
                .Include(v => v.Client)
                .Include(v => v.Commisioner)
                .Include(v => v.Department)
                .FirstOrDefaultAsync(m => m.VisitRegistrationId == id);
            if (visitregistration == null)
            {
                return NotFound();
            }

            return View(visitregistration);
        }

        // POST: Visitregistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitregistration = await _context.Visitregistration.FindAsync(id);
            _context.Visitregistration.Remove(visitregistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitregistrationExists(int id)
        {
            return _context.Visitregistration.Any(e => e.VisitRegistrationId == id);
        }
    }
}
