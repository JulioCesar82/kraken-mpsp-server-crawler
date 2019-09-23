using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

using KrakenMPSPBusiness.Context;
using KrakenMPSPBusiness.Models;

namespace KrakenMPSPServer.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class ResourcesFoundController : ControllerBase
    {
        private readonly SqlLiteContext _context;

        public ResourcesFoundController(SqlLiteContext context)
        {
            _context = context;
            _context.Database.MigrateAsync();
        }

        // GET: api/ResourcesFound
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourcesFound>>> GetResourcesFound()
        {
            return await _context.ResourcesFound.ToListAsync();
        }

        // GET: api/ResourcesFound/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourcesFound>> GetResourcesFound(long id)
        {
            var resourcesFound = await _context.ResourcesFound.FindAsync(id);

            if (resourcesFound == null)
            {
                return NotFound();
            }

            return resourcesFound;
        }

        // PUT: api/ResourcesFound/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResourcesFound(long id, ResourcesFound resourcesFound)
        {
            if (id != resourcesFound.Id)
            {
                return BadRequest();
            }

            _context.Entry(resourcesFound).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourcesFoundExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ResourceFound
        [HttpPost]
        public async Task<ActionResult<ResourcesFound>> PostResourcesFound(ResourcesFound resourcesFound)
        {
            _context.ResourcesFound.Add(resourcesFound);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResourcesFound", new { id = resourcesFound.Id }, resourcesFound);
        }

        // DELETE: api/ResourcesFound/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResourcesFound>> DeleteResourcesFound(long id)
        {
            var resourcesFound = await _context.ResourcesFound.FindAsync(id);
            if (resourcesFound == null)
            {
                return NotFound();
            }

            _context.ResourcesFound.Remove(resourcesFound);
            await _context.SaveChangesAsync();

            return resourcesFound;
        }

        private bool ResourcesFoundExists(long id)
        {
            return _context.ResourcesFound.Any(e => e.Id == id);
        }
    }
}
