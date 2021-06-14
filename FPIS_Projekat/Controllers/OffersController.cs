using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPIS_Projekat.Data;
using FPIS_Projekat.Models;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace FPIS_Projekat.Controllers
{
    public class OffersController : Controller
    {
        private readonly ISContext _context;

        public OffersController(ISContext context)
        {
            _context = context;
        }

        // GET: Offers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offers.ToListAsync());
        }

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {

            ViewBag.Employees = new List<Employee>(
                _context.Employees
                .Select(e => new Employee()
                {
                    ID = e.ID,
                    Name = e.Name
                })
               .ToList());

            ViewBag.Clients = new List<Client>(
                _context.Employees
                .Select(c => new Client()
                {
                    ID = c.ID,
                    Name = c.Name
                })
               .ToList());

            ViewBag.Devices = new List<Device>(
               _context.Devices
               .Include(d => d._Manufacturer)
               .Select(d => new Device()
               {
                   ID = d.ID,
                   Name = d.Name,
                   _Manufacturer = d._Manufacturer
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

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date")] Offer offer)
        {

            Employee e = new Employee()
            {
                ID = Convert.ToInt32(this.Request.Form["_Employee.Name"].ToArray()[0])
            };
            offer._Employee = e;

            offer._Client = new Client()
            {
                ID = Convert.ToInt32(this.Request.Form["_Client.Name"].ToArray()[0])
            };
            offer.OfferItems = new List<OfferItem>()
            {
                new OfferItem()
                {
                    _Offer = offer,
                    _Device = new Device()
                    {
                        ID = Convert.ToInt32(this.Request.Form["OfferItems[0]._Device.Name"].ToArray()[0])
                    },
                    _TariffPackage = new TariffPackage()
                    {
                        ID = Convert.ToInt32(this.Request.Form["OfferItems[0]._TariffPackage.Name"].ToArray()[0])
                    }
                }
            };

            //int? id = _context.Offers.Max(o => (int? )o.ID);

            //if (id == null)
            //    id = 0;

            //offer.ID = (int)id + 1;

            _context.Add(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date")] Offer offer)
        {
            if (id != offer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.ID))
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
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.ID == id);
        }

    }
}
