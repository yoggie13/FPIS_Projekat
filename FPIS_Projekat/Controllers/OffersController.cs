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
        public static List<OfferItem> offerItemsCreate = new List<OfferItem>();
        public static List<OfferItem> offerItemsEdit = new List<OfferItem>();
        public static bool listIsBeingEdited = false;
        //private User loggedInUser;


        private static bool done;

        public OffersController(ISContext context)
        {
            _context = ISContext.getContext();
        }


        // GET: Offers
        public async Task<IActionResult> Index()
        {
            //if (loggedInUser == null)
            //    return RedirectToAction("Login");
            //else
                return View();
        }

        [HttpGet, ActionName("Login")]
        public async Task<IActionResult> Login()
        {
            //if(loggedInUser != null)
            //{
                return RedirectToAction(nameof(Index));
            //}
            //return View("Login");
        }
        //[HttpPost, ActionName("User")]
        //public async Task<IActionResult> UserLogin(User user){

        //    User userDb = _context.Users
        //        .Where(u => u.Username == user.Username)
        //        .FirstOrDefault();

        //    if (userDb == null || userDb.Password != user.Password)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    loggedInUser = userDb;
        //    return RedirectToAction(nameof(Index));

        //}

        // GET: Offers/Create
        public IActionResult Create()
        {
            listIsBeingEdited = false;

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

            ViewBag.Items = offerItemsCreate;

            //ViewBag.Price = 0;
            //foreach (OfferItem of in offerItems)
            //{
            //    ViewBag.Price += of._Device.Price;
            //    ViewBag.Price += of._TariffPackage.Price;
            //}


            return View();

        }

        //POST: Offers/Search
        public async Task<IActionResult> Search([Bind("Date")] Offer offer)
        {
            listIsBeingEdited = false;

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
            listIsBeingEdited = false;

            offer._Employee = _context.Employees
                .Find(Convert.ToInt32(this.Request.Form["_Employee.Name"].ToArray()[0]));

            offer._Client = _context.Clients
                .Find(Convert.ToInt32(this.Request.Form["_Client.Name"].ToArray()[0]));

            offer.Date = offer.Date.Date;

            foreach (OfferItem o in offerItemsCreate)
            {
                o._Offer = offer;
            }
            offer.OfferItems = offerItemsCreate;

            _context.Add(offer);

            _context.SaveChanges();

            offerItemsCreate = new List<OfferItem>();

            return RedirectToAction(nameof(Index));

        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _context.ChangeTracker.Clear();
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
                     .AsNoTracking()
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
            if (listIsBeingEdited == false)
            {
                offerItemsEdit = offer.OfferItems;
                listIsBeingEdited = true;
            }

            ViewBag.Items = offerItemsEdit;

            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date")] Offer offer)
        {
            _context.ChangeTracker.Clear();

            if (id != offer.ID)
            {
                return NotFound();
            }

            try
            {
                var oridjidji = _context
                    .Offers
                    .Include(o => o.OfferItems)
                    .First(o => o.ID == offer.ID);

                oridjidji.OfferItems = offerItemsEdit;

                oridjidji._Employee = _context.Employees
                    .Where(e => e.ID == Convert.ToInt32(this.Request.Form["_Employee.ID"].ToArray()[0]))
                    .FirstOrDefault();

                oridjidji._Client = _context.Clients
                    .AsNoTracking()
                    .Where(c => c.ID == Convert.ToInt32(this.Request.Form["_Client.ID"].ToArray()[0]))
                    .FirstOrDefault();

                oridjidji.Date = offer.Date;

                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(offer.ID))
                {
                    return NotFound();
                }
                else
                {
                    offerItemsEdit = new List<OfferItem>();
                    return View(offer);
                }
            }

            offerItemsEdit = new List<OfferItem>();
            listIsBeingEdited = false;

            return RedirectToAction("Edit", new { id = offer.ID });

        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            listIsBeingEdited = false;

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
            listIsBeingEdited = false;

            var offer = await _context.Offers.FindAsync(id);
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.ID == id);
        }
        //[HttpGet, ActionName("CreateOfferItem")]
        //public async Task<ActionResult> CreateOfferItem()
        //{
        //    return PartialView(new OfferItem());
        //}

        [HttpPost, ActionName("CreateOfferItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOfferItem([Bind("ID")] OfferItem offerItem)
        {

            if (Request.Headers["Referer"].ToString().Contains("Create"))
            {
                offerItemsCreate.Add(new OfferItem()
                {
                    _Device = _context.Devices
                            .Find(Convert.ToInt32(this.Request.Form["_Device.ID"].ToArray()[0])),

                    _TariffPackage = _context.TariffPackages
                            .Find(Convert.ToInt32(this.Request.Form["_TariffPackage.ID"].ToArray()[0]))
                });
            }
            else if (Request.Headers["Referer"].ToString().Contains("Edit"))
            {
                offerItemsEdit.Add(new OfferItem()
                {
                    _Device = _context.Devices
                                       .Find(Convert.ToInt32(this.Request.Form["_Device.ID"].ToArray()[0])),

                    _TariffPackage = _context.TariffPackages
                                       .Find(Convert.ToInt32(this.Request.Form["_TariffPackage.ID"].ToArray()[0]))
                });
            }
            setDone(true);

            return Redirect(Request.Headers["Referer"]);
        }
        [HttpPost, ActionName("DeleteOfferItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOfferItem(int ide)
        {
            if (Request.Headers["Referer"].ToString().Contains("Create"))
                offerItemsCreate.RemoveAt(ide);
            else if (Request.Headers["Referer"].ToString().Contains("Edit"))
                offerItemsEdit.RemoveAt(ide);

            return Redirect(Request.Headers["Referer"]);

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
