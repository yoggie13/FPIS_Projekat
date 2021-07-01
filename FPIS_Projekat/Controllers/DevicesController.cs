using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FPIS_Projekat.Data;
using FPIS_Projekat.Models;

namespace FPIS_Projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ISContext _context;

        public DevicesController(ISContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices
                .Include(d => d._Manufacturer)
                .ToListAsync();
        }
        // GET: api/Devices/Manufacturers
        [HttpGet("Manufacturers")]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return await _context.Manufacturers
                .ToListAsync();
        }
        [HttpPost("{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Device>>> SearchDevices(string searchTerm)
        {
            return await _context.Devices
                .Include(d => d._Manufacturer)
                .Where(d => d.Name.Contains(searchTerm) || d._Manufacturer.Name.Contains(searchTerm))
                .ToListAsync();
        }

 

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.ID)
            {
                return BadRequest();
            }

            device._Manufacturer = _context.Manufacturers.Find(device._Manufacturer.ID);
            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            device._Manufacturer = _context.Manufacturers.Find(device._Manufacturer.ID);
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.ID }, device);
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.ID == id);
        }
    }
}
