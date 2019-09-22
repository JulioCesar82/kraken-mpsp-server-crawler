using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using KrakenMPSPBusiness.Context;
using KrakenMPSPBusiness.Models;

namespace KrakenMPSPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalPersonController : ControllerBase
    {
        private readonly SqlLiteContext _context;

        public PhysicalPersonController(SqlLiteContext context)
        {
            _context = context;
        }

        // GET: api/PhysicalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalPersonModel>>> GetPhysicalPerson()
        {
            return await _context.PhysicalPerson.ToListAsync();
        }

        // GET: api/PhysicalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> GetPhysicalPersonModel(long id)
        {
            var physicalPersonModel = await _context.PhysicalPerson.FindAsync(id);

            if (physicalPersonModel == null)
            {
                return NotFound();
            }

            return physicalPersonModel;
        }

        // PUT: api/PhysicalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhysicalPersonModel(long id, PhysicalPersonModel physicalPersonModel)
        {
            if (id != physicalPersonModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(physicalPersonModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalPersonModelExists(id))
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

        // POST: api/PhysicalPerson
        [HttpPost]
        public async Task<ActionResult<PhysicalPersonModel>> PostPhysicalPersonModel(PhysicalPersonModel physicalPersonModel)
        {
            _context.PhysicalPerson.Add(physicalPersonModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhysicalPersonModel", new { id = physicalPersonModel.Id }, physicalPersonModel);
        }

        // DELETE: api/PhysicalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> DeletePhysicalPersonModel(long id)
        {
            var physicalPersonModel = await _context.PhysicalPerson.FindAsync(id);
            if (physicalPersonModel == null)
            {
                return NotFound();
            }

            _context.PhysicalPerson.Remove(physicalPersonModel);
            await _context.SaveChangesAsync();

            return physicalPersonModel;
        }

        private bool PhysicalPersonModelExists(long id)
        {
            return _context.PhysicalPerson.Any(e => e.Id == id);
        }
    }
}
