﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
        List<ChatViewModel> jsonChats = new List<ChatViewModel>();

        public VisitregistrationsController(PihareiiContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
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

            var pihareiiContext = _context.Visitregistration
                .Include(v => v.Client)
                .Include(v => v.Commisioner)
                .Include(v => v.Department)
                .Include(v => v.StateVisitState)
                .Where(m => m.CommisionerId == idUser).ToList();

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
                .Include(v => v.StateVisitState)
                .FirstOrDefaultAsync(m => m.VisitRegistrationId == id);
            if (visitregistration == null)
            {
                return NotFound();
            }

            return View(visitregistration);
        }

        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName");
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "Nic");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentCode");
            ViewData["StateVisitStateId"] = new SelectList(_context.VisitState, "VisitStateId", "VisitStateValue");
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
                DepartmentId = idDepartment,
                CommisionerId = idCommisioner
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visitregistration visitregistration, string VisitDayDx)
        {

            if (!string.IsNullOrEmpty(VisitDayDx))
            {
                var date = DateTime.ParseExact(VisitDayDx.Substring(0, 24),
                                          "ddd MMM dd yyyy HH:mm:ss",
                                          CultureInfo.InvariantCulture);

                visitregistration.VisitDay = date;
            }

            if (visitregistration.ClientId == 0)
            {
                Client client = JsonConvert.DeserializeObject<Client>(visitregistration.ClientJson);
                var Commisioner = _context.Commisioner.FirstOrDefault(m => m.CommisionerId == visitregistration.CommisionerId);

                client.Commisioner = Commisioner;
                client.RegistredDate = DateTime.UtcNow.AddHours(-4);
                visitregistration.Client = client;
            }

            try
            {
                visitregistration.StateVisitStateId = 1;

                if (string.IsNullOrEmpty(visitregistration.VisitDay.ToString()) && string.IsNullOrEmpty(visitregistration.Observations))
                {
                    visitregistration.StateVisitStateId = 4;
                }


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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", visitregistration.ClientId);
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "CommisionerPassword", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentDescription", visitregistration.DepartmentId);
            ViewData["StateVisitStateId"] = new SelectList(_context.VisitState, "VisitStateId", "VisitStateValue", visitregistration.StateVisitStateId);
            return View(visitregistration);
        }

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

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", visitregistration.ClientId);
            ViewData["CommisionerId"] = new SelectList(_context.Commisioner, "CommisionerId", "Nic", visitregistration.CommisionerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentCode", visitregistration.DepartmentId);
            ViewData["StateVisitStateId"] = new SelectList(_context.VisitState, "VisitStateId", "VisitStateValue", visitregistration.StateVisitStateId);
            return View(visitregistration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Visitregistration visitregistration, string VisitDayDx)
        {
            if (id != visitregistration.VisitRegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var date = DateTime.ParseExact(VisitDayDx.Substring(0, 24),
                          "ddd MMM dd yyyy HH:mm:ss",
                          CultureInfo.InvariantCulture);

                    visitregistration.VisitDay = date;

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
            ViewData["StateVisitStateId"] = new SelectList(_context.VisitState, "VisitStateId", "VisitStateValue", visitregistration.StateVisitStateId);
            return View(visitregistration);
        }

        public async Task<IActionResult> CommisionerEdit(int? id)
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
            return View(visitregistration);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommisionerEdit(int id, Visitregistration visitregistration, string VisitDayDx)
        {
            if (id != visitregistration.VisitRegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var date = DateTime.ParseExact(VisitDayDx.Substring(0, 24),
                          "ddd MMM dd yyyy HH:mm:ss",
                          CultureInfo.InvariantCulture);

                    visitregistration.VisitDay = date;

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
                return RedirectToAction("MyVisits");
            }
            return View(visitregistration);
        }

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

        [HttpPost]
        public async Task<IActionResult> ChatMessages(Chat data)
        {
            if (!string.IsNullOrEmpty(data.Message))
            {
                var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);
                var dateUTC4 = DateTime.UtcNow.AddHours(-4);

                data.CommisionerId = idUser;
                data.MessageTime = dateUTC4.AddHours(-4);

                if (ModelState.IsValid)
                {
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult ChatMessages(int visitId)
        {

            var chats = _context.Chat.Include(v => v.Commisioner).Where(v => v.VisitId.Equals(visitId)).ToList();

            foreach (Chat item in chats)
            {
                jsonChats.Add(new ChatViewModel
                {
                    ChatId = item.ChatId,
                    Message = item.Message,
                    MessageTime = item.MessageTime.ToString("dd/MM/yyyy HH:mm "),
                    AutorFullName = $"{item.Commisioner.FirstName} {item.Commisioner.LastName} {item.Commisioner.SecondLastName}",
                    VisitId = item.VisitId,
                    CommisionerId = item.CommisionerId
                }); ;
            }

            string JsonContext = JsonConvert.SerializeObject(jsonChats, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(JsonContext);
        }


        private bool VisitregistrationExists(int id)
        {
            return _context.Visitregistration.Any(e => e.VisitRegistrationId == id);
        }
    }
}
