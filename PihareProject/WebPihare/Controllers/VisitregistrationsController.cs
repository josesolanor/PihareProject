using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebPihare.Context;
using WebPihare.Entities;
using WebPihare.Models;

namespace WebPihare.Controllers
{
    [Authorize]
    public class VisitregistrationsController : Controller
    {
        private readonly PihareiiContext _context;

        List<RegisterViewModel> jsonList = new List<RegisterViewModel>();


        public VisitregistrationsController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Visitregistrations
        public IActionResult Index()
        {
            ViewData["VisitStateId"] = new SelectList(_context.VisitState, "VisitStateId", "VisitStateValue");
            return View();
        }

        public IActionResult MyVisits()
        {
            return View();
        }

        public IActionResult LoadGrid()
        {
            var pihareiiContext = _context.Visitregistration.Include(v => v.Client).Include(v => v.Commisioner).Include(v => v.Department).Include(v => v.StateVisitState).ToList();

            foreach (Visitregistration item in pihareiiContext)
            {
                jsonList.Add(new RegisterViewModel
                {
                    VisitDay = item.VisitDay,
                    Observations = item.Observations,
                    VisitRegistrationId = item.VisitRegistrationId,
                    ClientId = item.ClientId,
                    CommisionerId = item.CommisionerId,
                    DepartmentId = item.DepartmentId,
                    FullNameClient = $"{item.Client.FirstName} {item.Client.LastName} {item.Client.SecondLastName}",
                    FullNameCommisioner = $"{item.Commisioner.FirstName} {item.Commisioner.LastName} {item.Commisioner.SecondLastName}",
                    DepartmentCode = item.Department.DepartmentCode,
                    State = item.StateVisitState.VisitStateValue
                });
            }

            string JsonContext = JsonConvert.SerializeObject(jsonList, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(JsonContext);
        }
        public IActionResult MyLoadGrid()
        {
            var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

            var pihareiiContext = _context.Visitregistration.Include(v => v.Client).Include(v => v.Commisioner).Include(v => v.Department).Where(m => m.Commisioner.CommisionerId == idUser).ToList();

            foreach (Visitregistration item in pihareiiContext)
            {
                jsonList.Add(new RegisterViewModel
                {
                    VisitDay = item.VisitDay,
                    Observations = item.Observations,
                    VisitRegistrationId = item.VisitRegistrationId,
                    ClientId = item.ClientId,
                    CommisionerId = item.CommisionerId,
                    DepartmentId = item.DepartmentId,
                    FullNameClient = $"{item.Client.FirstName} {item.Client.LastName} {item.Client.SecondLastName}",
                    FullNameCommisioner = $"{item.Commisioner.FirstName} {item.Commisioner.LastName} {item.Commisioner.SecondLastName}",
                    DepartmentCode = item.Department.DepartmentCode,
                    State = item.StateVisitState.VisitStateValue
                });
            }

            string JsonContext = JsonConvert.SerializeObject(jsonList, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(JsonContext);
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
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName");
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "Nic");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentCode");
            return View();
        }

        [HttpGet]
        public IActionResult RegisterCreate(string clientJson, int idCommisioner, int idDepartment)
        {

            Client client = JsonConvert.DeserializeObject<Client>(clientJson);

            Visitregistration model = new Visitregistration
            {
                Commisioner = _context.Commisioner.Include(v => v.Role).FirstOrDefault(m => m.CommisionerId == idCommisioner),
                ClientJson = clientJson,
                Client = client,
                Department = _context.Department.Include(v => v.DepartmentState).Include(v => v.DepartmentType).FirstOrDefault(m => m.DepartmentId == idDepartment),
                VisitDay = DateTime.Now,
                DepartmentId = idDepartment,
                CommisionerId = idCommisioner
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visitregistration visitregistration)
        {

            Client client = JsonConvert.DeserializeObject<Client>(visitregistration.ClientJson);

            var Commisioner = _context.Commisioner.FirstOrDefault(m => m.CommisionerId == visitregistration.CommisionerId);

            client.Commisioner = Commisioner;
            client.RegistredDate = DateTime.Now;

            try
            {
                visitregistration.Client = client;

                if (ModelState.IsValid)
                {
                    _context.Add(visitregistration);
                    await _context.SaveChangesAsync();

                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return RedirectToAction("MyVisits");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMsg"] = "Cliente ya registrado";
                return RedirectToAction("Index", "Departments");
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
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "Nic", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentCode", visitregistration.DepartmentId);
            return View(visitregistration);
        }

        // POST: Visitregistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Visitregistration visitregistration)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(AddVisitState data)
        {
            var visit = _context.Visitregistration.FirstOrDefault(m => m.VisitRegistrationId == data.visitSeletedId);

            visit.StateVisitStateId = data.stateId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitregistrationExists(visit.VisitRegistrationId))
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
            TempData["ErrorMsg"] = "No se puede añadir estado";
            return RedirectToAction("Index", "Departments");
        }

        private bool VisitregistrationExists(int id)
        {
            return _context.Visitregistration.Any(e => e.VisitRegistrationId == id);
        }
    }
}
