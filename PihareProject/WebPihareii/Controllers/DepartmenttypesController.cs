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
    public class DepartmenttypesController : Controller
    {
        private readonly PihareiiContext _context;

        public DepartmenttypesController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Departmenttypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departmenttype.ToListAsync());
        }

        // GET: Departmenttypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmenttype = await _context.Departmenttype
                .FirstOrDefaultAsync(m => m.DepartmentTypeId == id);
            if (departmenttype == null)
            {
                return NotFound();
            }

            return View(departmenttype);
        }

        // GET: Departmenttypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departmenttypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentTypeId,DepartmentTypeValue,DepartmentTypeDescription")] Departmenttype departmenttype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmenttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmenttype);
        }

        // GET: Departmenttypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmenttype = await _context.Departmenttype.FindAsync(id);
            if (departmenttype == null)
            {
                return NotFound();
            }
            return View(departmenttype);
        }

        // POST: Departmenttypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentTypeId,DepartmentTypeValue,DepartmentTypeDescription")] Departmenttype departmenttype)
        {
            if (id != departmenttype.DepartmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmenttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmenttypeExists(departmenttype.DepartmentTypeId))
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
            return View(departmenttype);
        }

        // GET: Departmenttypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmenttype = await _context.Departmenttype
                .FirstOrDefaultAsync(m => m.DepartmentTypeId == id);
            if (departmenttype == null)
            {
                return NotFound();
            }

            return View(departmenttype);
        }

        // POST: Departmenttypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmenttype = await _context.Departmenttype.FindAsync(id);
            _context.Departmenttype.Remove(departmenttype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmenttypeExists(int id)
        {
            return _context.Departmenttype.Any(e => e.DepartmentTypeId == id);
        }
    }
}
