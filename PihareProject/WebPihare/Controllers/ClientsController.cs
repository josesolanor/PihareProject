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

            var client = await _context.Client
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                var idUser = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

                var Commisioner = _context.Commisioner.FirstOrDefault(m => m.CommisionerId == idUser);

                client.Commisioner = Commisioner;
                client.RegistredDate = DateTime.Now;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
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
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LoadGrid()
        {

            List<DepartmentClientViewModel> departmentClient = new List<DepartmentClientViewModel>();
            List<ClientViewModel> clients = new List<ClientViewModel>();

            var client = _context.Client.Include(v => v.Commisioner).ToList();
            var visitregistration = _context.Visitregistration
                .Include(v => v.Department)
                .Include(m => m.Department.DepartmentState)
                .Include(m => m.Department.DepartmentType).ToList();

            foreach (var item in visitregistration)
            {
                departmentClient.Add(new DepartmentClientViewModel
                {
                    ClientId = item.ClientId,
                    DepartmentId = item.DepartmentId,
                    DeparmentPrice = item.Department.DeparmentPrice,
                    DepartmentCode = item.Department.DepartmentCode,
                    NumberBedrooms = item.Department.NumberBedrooms,
                    NumberFloor = item.Department.NumberFloor,
                    DepartmentStateId = item.Department.DepartmentStateId,
                    DepartmentTypeId = item.Department.DepartmentTypeId,
                    DepartmentState = item.Department.DepartmentState.DepartmentStateValue,
                    DepartmentType = item.Department.DepartmentType.DepartmentTypeValue
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
                    CI = item.CI,
                    CommisionerId = item.CommisionerId,
                    RegistredDate = item.RegistredDate,
                    CommisionerFullName = item.Commisioner.FullName
                });
            }

            var result = new { Master = clients, Detail = departmentClient };
            return Json(result);
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
