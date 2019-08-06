using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPihare.Context;
using WebPihare.Entities;

namespace WebPihare.Controllers
{
    public class CommisionersController : Controller
    {
        private readonly PihareiiContext _context;

        public CommisionersController(PihareiiContext context)
        {
            _context = context;
        }

        // GET: Commisioners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commisioner.ToListAsync());
        }

        // GET: Commisioners/Details/5
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

        // GET: Commisioners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commisioners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommisionerId,FirstName,LastName,SecondLastName,Nic,ContractNumber,Email,CommisionerPassword")] Commisioner commisioner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commisioner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commisioner);
        }

        // GET: Commisioners/Edit/5
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

        // POST: Commisioners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Commisioners/Delete/5
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

        // POST: Commisioners/Delete/5
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
