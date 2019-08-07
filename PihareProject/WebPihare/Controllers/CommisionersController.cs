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
            return View(await _context.Commisioner.ToListAsync());
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommisionerId,FirstName,LastName,SecondLastName,Nic,ContractNumber,Email,CommisionerPassword")] Commisioner commisioner)
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
            return View(commisioner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommisionerId,FirstName,LastName,SecondLastName,Nic,ContractNumber,Email,CommisionerPassword")] Commisioner commisioner)
        {
            if (id != commisioner.CommisionerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commisioner.CommisionerPassword = _hash.EncryptString(commisioner.CommisionerPassword);
                    _context.Update(commisioner);
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
