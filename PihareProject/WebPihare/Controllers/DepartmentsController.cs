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
    public class DepartmentsController : Controller
    {
        private readonly PihareiiContext _context;

        public DepartmentsController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            RegisterModalClientViewModal model = new RegisterModalClientViewModal
            {
                Departments = await _context.Department.Include(d => d.DepartmentState).Include(d => d.DepartmentType).ToListAsync()
            };
            return View(model);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.DepartmentState)
                .Include(d => d.DepartmentType)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["DepartmentStateId"] = new SelectList(_context.Departmentstate, "DepartmentStateId", "DepartmentStateDescription");
            ViewData["DepartmentTypeId"] = new SelectList(_context.Departmenttype, "DepartmentTypeId", "DepartmentTypeDescription");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClient(RegisterModalClientViewModal data, int DepartmentIdSelected)
        {
            if (ModelState.IsValid)
            {

                var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

                data.Client.Commisioner.CommisionerId = idUser;

                _context.Add(data.Client);
                await _context.SaveChangesAsync();

                return RedirectToAction("");
            }
            RegisterModalClientViewModal model = new RegisterModalClientViewModal
            {
                Departments = await _context.Department.Include(d => d.DepartmentState).Include(d => d.DepartmentType).ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentCode,NumberFloor,NumberBedrooms,DepartmentDescription,DeparmentPrice,DepartmentTypeId,DepartmentStateId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentStateId"] = new SelectList(_context.Departmentstate, "DepartmentStateId", "DepartmentStateDescription", department.DepartmentStateId);
            ViewData["DepartmentTypeId"] = new SelectList(_context.Departmenttype, "DepartmentTypeId", "DepartmentTypeDescription", department.DepartmentTypeId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["DepartmentStateId"] = new SelectList(_context.Departmentstate, "DepartmentStateId", "DepartmentStateDescription", department.DepartmentStateId);
            ViewData["DepartmentTypeId"] = new SelectList(_context.Departmenttype, "DepartmentTypeId", "DepartmentTypeDescription", department.DepartmentTypeId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentCode,NumberFloor,NumberBedrooms,DepartmentDescription,DeparmentPrice,DepartmentTypeId,DepartmentStateId")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            ViewData["DepartmentStateId"] = new SelectList(_context.Departmentstate, "DepartmentStateId", "DepartmentStateDescription", department.DepartmentStateId);
            ViewData["DepartmentTypeId"] = new SelectList(_context.Departmenttype, "DepartmentTypeId", "DepartmentTypeDescription", department.DepartmentTypeId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.DepartmentState)
                .Include(d => d.DepartmentType)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
