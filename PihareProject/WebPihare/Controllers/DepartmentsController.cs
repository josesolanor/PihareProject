using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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

        [Authorize(Roles="Admin,Comisionista")]
        public async Task<IActionResult> Index()
        {
            RegisterModalClientViewModal model = new RegisterModalClientViewModal
            {
                Departments = await _context.Department.Include(d => d.DepartmentState).Include(d => d.DepartmentType).ToListAsync()
            };
            ViewData["Provenance"] = new SelectList(_context.Provenance, "ProvenanceValue", "ProvenanceValue");
            return View(model);
        }

        // GET: Departments/Details/5
        [Authorize(Roles = "Admin,Comisionista")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DepartmentStateId"] = new SelectList(_context.Departmentstate, "DepartmentStateId", "DepartmentStateValue");
            ViewData["DepartmentTypeId"] = new SelectList(_context.Departmenttype, "DepartmentTypeId", "DepartmentTypeValue");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Comisionista")]
        public IActionResult RegisterClient(RegisterModalClientViewModal data)
        {

            if (ModelState.IsValid)
            {
                var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

                var exist = _context.Client.FirstOrDefault(m => m.CI == data.Client.CI);

                if (exist == null)
                {

                    string ClientJson = JsonConvert.SerializeObject(data.Client, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                    return RedirectToAction("RegisterCreate", "Visitregistrations", new { clientJson = ClientJson, idCommisioner = idUser, idDepartment = data.DepartmentIdSelected });
                }

            }
            TempData["ErrorMsg"] = $"Error, El cliente con CI: {data.Client.CI} ya se encuentra registrado";
            return RedirectToAction("Index", "Departments");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Department department)
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

        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Department department)
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

        [Authorize(Roles = "Admin")]
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

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);            
            try
            {
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = $"Error, El Departamento {department.DepartmentCode} no se puede eliminar, se encuentra siendo usado";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(RegisterModalClientViewModal data)
        {

            var department = _context.Department.FirstOrDefault(m => m.DepartmentId == data.DepartmentIdCommentSelected);

            department.Comments = data.DepartmentComment;

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
            TempData["ErrorMsg"] = "No se puede añadir comentario";
            return RedirectToAction("Index", "Departments");
        }
    }
}
