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
using System.Data;
using System.ComponentModel;
using System.Text.Json;

namespace FPIS_Projekat.Controllers
{
    public class OffersController : Controller
    {
        private readonly ISContext _context;
        public static List<OfferItem> offerItems = new List<OfferItem>();
        private static bool done;

        public OffersController(ISContext context)
        {
            _context = ISContext.getContext();
        }


        // GET: Offers
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o._Employee)
                .Include(o => o._Client)
                .Include(o => o.OfferItems)
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
                _context.Clients
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

            ViewBag.Items = offerItems;

            ViewBag.Price = 0;
            foreach (OfferItem of in offerItems)
            {
                ViewBag.Price += of._Device.Price;
                ViewBag.Price += of._TariffPackage.Price;
            }


            return View();

        }

        //POST: Offers/Search
        public async Task<IActionResult> Search([Bind("Date")] Offer offer)
        {

            
            ViewBag.Offers = _context.Offers
                .Where(o => o.Date == offer.Date.Date)
                .Include(o => o._Employee)
                .Include(o => o._Client);

            return View();

        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date")] Offer offer)
        {

            offer._Employee = _context.Employees
                .Find(Convert.ToInt32(this.Request.Form["_Employee.Name"].ToArray()[0]));

            offer._Client = _context.Clients
                .Find(Convert.ToInt32(this.Request.Form["_Client.Name"].ToArray()[0]));

            offer.Date = offer.Date.Date;

            foreach (OfferItem o in offerItems)
            {
                o._Offer = offer;
            }
            offer.OfferItems = offerItems;

            var query = _context.Add(offer);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Employees = new List<Employee>(
                _context.Employees
                .Select(e => new Employee()
                {
                    ID = e.ID,
                    Name = e.Name
                })
               .ToList());

            ViewBag.Clients = new List<Client>(
                _context.Clients
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


            var offer = await _context.Offers
                     .Include(o => o._Employee)
                     .Include(o => o._Client)
                     .Include(o => o.OfferItems)
                        .ThenInclude(of => of._Device)
                     .Include(o => o.OfferItems)
                       .ThenInclude(of => of._TariffPackage)
                     .FirstOrDefaultAsync(m => m.ID == id);

            if (offer == null)
            {
                return NotFound();
            }
            offerItems = offer.OfferItems;
            ViewBag.Items = offerItems;

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
                    _context.OfferItems
                        .RemoveRange(_context.OfferItems.Where(o => o._Offer.ID == offer.ID));
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

        [HttpGet, ActionName("LoadItems")]
        public async Task<string> loadItems()
        {
            await getDone();
            setDone(false);

            //ViewBag.Items = offerItems;

            //ViewBag.Price = 0;
            //foreach (OfferItem of in offerItems)
            //{
            //    ViewBag.Price += of._Device.Price;
            //}



            //return PartialView("TableOfferItems");

            return JsonSerializer.Serialize<OfferItem>(offerItems.LastOrDefault());
        }
        [HttpPost, ActionName("CreateOfferItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOfferItem([Bind("ID")] OfferItem offerItem)
        {

            offerItems.Add(new OfferItem()
            {
                _Device = _context.Devices
                        .Find(Convert.ToInt32(this.Request.Form["OfferItems[0]._Device.ID"].ToArray()[0])),

                _TariffPackage = _context.TariffPackages
                        .Find(Convert.ToInt32(this.Request.Form["OfferItems[0]._TariffPackage.ID"].ToArray()[0]))
            });

            setDone(true);

            return RedirectToAction("Create");
        }
        [HttpPost, ActionName("DeleteOfferItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOfferItem(int id)
        {

            offerItems.RemoveAt(id);

            //return RedirectToAction(nameof(Create));
            return new EmptyResult();

        }
        public static void setDone(bool val)
        {
            done = val;
        }

        public static async Task<bool> getDone()
        {
            while (done == false)
            {
            }
            return done;
        }

    }
}
