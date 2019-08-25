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
using WebPihare.Core;
using WebPihare.Models;

namespace WebPihare.Controllers
{
    [Authorize]
    public class CommisionersController : Controller
    {
        private readonly PihareiiContext _context;
        private readonly Hash _hash;

        public CommisionersController(PihareiiContext context, Hash hash)
        {
            _context = context;
            _hash = hash;
        }
        
        public async Task<IActionResult> Index()
        {
            var pihareiiContext = _context.Commisioner.Include(d => d.Role);
            return View(await pihareiiContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commisioner = await _context.Commisioner
                .FirstOrDefaultAsync(m => m.CommisionerId == id);
            if (commisioner == null)
            {
                return NotFound();
            }

            return View(commisioner);
        }

        public async Task<IActionResult> MyProfile()
        {
            MyProfile myprofile = new MyProfile();

            var id = int.Parse(User.Claims.FirstOrDefault(m => m.Type == "Id").Value);

            if (id == 0)
            {
                return NotFound();
            }

            var commisioner = await _context.Commisioner
                .FirstOrDefaultAsync(m => m.CommisionerId == id);
            if (commisioner == null)
            {
                return NotFound();
            }

            myprofile.Commisioner = commisioner;

            return View(myprofile);
        }

        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleValue");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Commisioner commisioner)
        {
            if (ModelState.IsValid)
            {
                commisioner.CommisionerPassword = _hash.EncryptString(commisioner.CommisionerPassword);
                _context.Add(commisioner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commisioner);
        }

        [HttpPost]
        public async Task<IActionResult> AdminChangePassword(string NewPassword, int CommisionerIdSelected)
        {
            var commisioner = _context.Commisioner.FirstOrDefault(v => v.CommisionerId.Equals(CommisionerIdSelected));

            commisioner.CommisionerPassword = _hash.EncryptString(NewPassword);

            _context.Update(commisioner);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Commisioners");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commisioner = await _context.Commisioner.FindAsync(id);
            if (commisioner == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleValue");
            return View(commisioner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Commisioner commisioner)
        {
            if (id != commisioner.CommisionerId)
            {
                return NotFound();
            }

            var updateCommisioner = _context.Commisioner.FirstOrDefault(v => v.CommisionerId.Equals(id));

            updateCommisioner.FirstName = commisioner.FirstName;
            updateCommisioner.LastName = commisioner.LastName;
            updateCommisioner.SecondLastName = commisioner.SecondLastName;
            updateCommisioner.Nic = commisioner.Nic;
            updateCommisioner.ContractNumber = commisioner.ContractNumber;
            updateCommisioner.Email = commisioner.Email;
            updateCommisioner.Telefono = commisioner.Telefono;
            updateCommisioner.RoleId = commisioner.RoleId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updateCommisioner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommisionerExists(commisioner.CommisionerId))
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
            return View(commisioner);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commisioner = await _context.Commisioner
                .FirstOrDefaultAsync(m => m.CommisionerId == id);
            if (commisioner == null)
            {
                return NotFound();
            }

            return View(commisioner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commisioner = await _context.Commisioner.FindAsync(id);
            _context.Commisioner.Remove(commisioner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommisionerExists(int id)
        {
            return _context.Commisioner.Any(e => e.CommisionerId == id);
        }
    }
}
