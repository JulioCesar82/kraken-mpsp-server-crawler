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
    public class LegalPersonController : ControllerBase
    {
        private readonly SqlLiteContext _context;

        public LegalPersonController(SqlLiteContext context)
        {
            _context = context;
            _context.Database.MigrateAsync();
        }

        // GET: api/LegalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegalPersonModel>>> GetLegalPerson()
        {
            return await _context.LegalPerson.ToListAsync();
        }

        // GET: api/LegalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalPersonModel>> GetLegalPersonModel(long id)
        {
            var legalPersonModel = await _context.LegalPerson.FindAsync(id);

            if (legalPersonModel == null)
            {
                return NotFound();
            }

            return legalPersonModel;
        }

        // PUT: api/LegalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLegalPersonModel(long id, LegalPersonModel legalPersonModel)
        {
            if (id != legalPersonModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(legalPersonModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegalPersonModelExists(id))
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

        // POST: api/LegalPerson
        [HttpPost]
        public async Task<ActionResult<LegalPersonModel>> PostLegalPersonModel(LegalPersonModel legalPersonModel)
        {
            _context.LegalPerson.Add(legalPersonModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLegalPersonModel", new { id = legalPersonModel.Id }, legalPersonModel);
        }

        // DELETE: api/LegalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LegalPersonModel>> DeleteLegalPersonModel(long id)
        {
            var legalPersonModel = await _context.LegalPerson.FindAsync(id);
            if (legalPersonModel == null)
            {
                return NotFound();
            }

            _context.LegalPerson.Remove(legalPersonModel);
            await _context.SaveChangesAsync();

            return legalPersonModel;
        }

        private bool LegalPersonModelExists(long id)
        {
            return _context.LegalPerson.Any(e => e.Id == id);
        }
    }
}
