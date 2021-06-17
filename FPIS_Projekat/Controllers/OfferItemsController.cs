using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPIS_Projekat.Data;
using FPIS_Projekat.Models;

namespace FPIS_Projekat.Controllers
{
    public class OfferItemsController : Controller
    {
        private readonly ISContext _context;
        private static bool done;

        public OfferItemsController(ISContext context)
        {
            _context = ISContext.getContext();
        }

        // GET: OfferItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfferItems.ToListAsync());
        }

        // GET: OfferItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerItem = await _context.OfferItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (offerItem == null)
            {
                return NotFound();
            }

            return View(offerItem);
        }

        // GET: OfferItems/Create
        public IActionResult Create()
        {
            ViewBag.Devices = new List<Device>(
               _context.Devices
               .Include(d => d._Manufacturer)
               .Select(d => new Device()
               {
                   ID = d.ID,
                   Name = d.Name,
                   _Manufacturer = d._Manufacturer,
                   Price = d.Price
               })
               .ToList());

            ViewBag.Packages = new List<TariffPackage>(
              _context.TariffPackages
              .Select(t => new TariffPackage()
              {
                  ID = t.ID,
                  Name = t.Name
              })
              .ToList());

            return View();
        }

        // POST: OfferItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] OfferItem offerItem)
        {
            OffersController.offerItems.Add(new OfferItem()
            {
                _Device = _context.Devices
                        .Find(Convert.ToInt32(this.Request.Form["_Device.ID"].ToArray()[0])),

                _TariffPackage = _context.TariffPackages
                        .Find(Convert.ToInt32(this.Request.Form["_TariffPackage.ID"].ToArray()[0]))
            });

            setDone(true);

            return RedirectToAction(nameof(Create));
        }
        public static void setDone(bool val)
        {
            done = val;
        }

        public static async Task<bool> getDone()
        {
            while(done == false)
            {
            }
            return done;
        }
        

        // GET: OfferItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerItem = await _context.OfferItems.FindAsync(id);
            if (offerItem == null)
            {
                return NotFound();
            }
            return View(offerItem);
        }

        // POST: OfferItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] OfferItem offerItem)
        {
            if (id != offerItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offerItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferItemExists(offerItem.ID))
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
            return View(offerItem);
        }

        // GET: OfferItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerItem = await _context.OfferItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (offerItem == null)
            {
                return NotFound();
            }

            return View(offerItem);
        }

        // POST: OfferItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {
            //var offerItem = await _context.OfferItems.FindAsync(id);
            //_context.OfferItems.Remove(offerItem);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            OffersController.offerItems.RemoveAt(id);
        }

        private bool OfferItemExists(int id)
        {
            return _context.OfferItems.Any(e => e.ID == id);
        }

    }
}
