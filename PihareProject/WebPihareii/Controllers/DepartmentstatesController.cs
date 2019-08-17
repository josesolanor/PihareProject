using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPihareii.Context;
using WebPihareii.Entities;

namespace WebPihareii.Controllers
{
    public class DepartmentstatesController : Controller
    {
        private readonly PihareiiContext _context;

        public DepartmentstatesController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Departmentstates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departmentstate.ToListAsync());
        }

        // GET: Departmentstates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentstate = await _context.Departmentstate
                .FirstOrDefaultAsync(m => m.DepartmentStateId == id);
            if (departmentstate == null)
            {
                return NotFound();
            }

            return View(departmentstate);
        }

        // GET: Departmentstates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departmentstates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentStateId,DepartmentStateValue,DepartmentStateDescription")] Departmentstate departmentstate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentstate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentstate);
        }

        // GET: Departmentstates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentstate = await _context.Departmentstate.FindAsync(id);
            if (departmentstate == null)
            {
                return NotFound();
            }
            return View(departmentstate);
        }

        // POST: Departmentstates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentStateId,DepartmentStateValue,DepartmentStateDescription")] Departmentstate departmentstate)
        {
            if (id != departmentstate.DepartmentStateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentstate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentstateExists(departmentstate.DepartmentStateId))
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
            return View(departmentstate);
        }

        // GET: Departmentstates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentstate = await _context.Departmentstate
                .FirstOrDefaultAsync(m => m.DepartmentStateId == id);
            if (departmentstate == null)
            {
                return NotFound();
            }

            return View(departmentstate);
        }

        // POST: Departmentstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentstate = await _context.Departmentstate.FindAsync(id);
            _context.Departmentstate.Remove(departmentstate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentstateExists(int id)
        {
            return _context.Departmentstate.Any(e => e.DepartmentStateId == id);
        }
    }
}
