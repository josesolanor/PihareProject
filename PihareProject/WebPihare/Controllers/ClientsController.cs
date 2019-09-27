﻿using System;
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
    public class ClientsController : Controller
    {
        private readonly PihareiiContext _context;

        public ClientsController(PihareiiContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var model = await _context.Client.Include(m => m.Commisioner).Include(m => m.Visitregistration).ToListAsync();
            return View(model);
        }

        [Authorize(Roles = "Comisionista")]
        public async Task<IActionResult> MyClients()
        {
            var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);
            return View(await _context.Client.Include(m => m.Commisioner).Where(m => m.CommisionerId == idUser).ToListAsync());
        }

        [Authorize(Roles = "Admin, Comisionista")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.Include(v => v.Commisioner)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Provenance"] = new SelectList(_context.Provenance, "ProvenanceValue", "ProvenanceValue");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                var exist = _context.Client.FirstOrDefault(m => m.CI.Equals(client.CI));

                if (exist == null)
                {
                    var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

                    var Commisioner = _context.Commisioner.FirstOrDefault(m => m.CommisionerId == idUser);

                    client.Commisioner = Commisioner;
                    client.RegistredDate = DateTime.UtcNow.AddHours(-4);
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["ErrorMsg"] = $"Error, El cliente con CI: {client.CI} ya se encuentra registrado";
            return View(client);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.Include(v => v.Commisioner)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            try
            {                
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = $"Error, El cliente {client.FullName} no se puede eliminar, se encuentra siendo usado";
                return RedirectToAction(nameof(Index));
            }
            
        }

        public IActionResult LoadGrid()
        {
            List<ClientViewModel> clients = new List<ClientViewModel>();
            List<VisitClientModel> visitClient = new List<VisitClientModel>();

            var client = _context.Client.Include(v => v.Commisioner).ToList();
            var visitregistration = _context.Visitregistration
                .Include(v => v.Department)
                .Include(v => v.StateVisitState)
                .Include(v => v.Commisioner)
                .Include(m => m.Department.DepartmentState)
                .Include(m => m.Department.DepartmentType).ToList();

            foreach (var item in visitregistration)
            {
                visitClient.Add(new VisitClientModel
                {
                    VisitRegistrationId = item.VisitRegistrationId,
                    ClientId = item.ClientId,
                    VisitDay = item.VisitDay,
                    StateVisitStateValue = item.StateVisitState.VisitStateValue,
                    DepartmentCode = item.Department.DepartmentCode,
                    CommisionerFullName = item.Commisioner.FullName,
                });
            }

            foreach (var item in client)
            {
                clients.Add(new ClientViewModel
                {
                    ClientId = item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    SecondLastName = item.SecondLastName,
                    Observation = item.Observation,
                    CI = $"{ item.CI}-{item.Provenance}",
                    CommisionerId = item.CommisionerId,
                    RegistredDate = item.RegistredDate,
                    CommisionerFullName = item.Commisioner.FullName
                });
            }

            var result = new { Master = clients, Detail = visitClient };
            return Json(result);
        }

        public IActionResult MyLoadGrid()
        {
            var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);
            List<ClientViewModel> clients = new List<ClientViewModel>();
            List<VisitClientModel> visitClient = new List<VisitClientModel>();

            var client = _context.Client.Include(v => v.Commisioner).Where(v => v.CommisionerId.Equals(idUser)).ToList();
            var visitregistration = _context.Visitregistration
                .Include(v => v.Department)
                .Include(v => v.StateVisitState)
                .Include(v => v.Commisioner)
                .Include(m => m.Department.DepartmentState)
                .Include(m => m.Department.DepartmentType)
                .Where(m => m.CommisionerId.Equals(idUser))
                .ToList();

            foreach (var item in visitregistration)
            {
                visitClient.Add(new VisitClientModel
                {
                    VisitRegistrationId = item.VisitRegistrationId,
                    ClientId = item.ClientId,
                    VisitDay = item.VisitDay,
                    StateVisitStateValue = item.StateVisitState.VisitStateValue,
                    DepartmentCode = item.Department.DepartmentCode,
                    CommisionerFullName = item.Commisioner.FullName,
                });
            }

            foreach (var item in client)
            {
                clients.Add(new ClientViewModel
                {
                    ClientId = item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    SecondLastName = item.SecondLastName,
                    Observation = item.Observation,
                    CI = $"{ item.CI}-{item.Provenance}",
                    CommisionerId = item.CommisionerId,
                    RegistredDate = item.RegistredDate,
                    CommisionerFullName = item.Commisioner.FullName
                });
            }



            var result = new { Master = clients, Detail = visitClient };
            return Json(result);
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
