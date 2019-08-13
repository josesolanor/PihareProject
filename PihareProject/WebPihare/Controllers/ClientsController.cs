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
            return View(await _context.Client.Include(m => m.Commisioner).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("ClientId,FirstName,LastName,SecondLastName,Observation,CI,Telefono,RegistredDate")] Client client)
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

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
