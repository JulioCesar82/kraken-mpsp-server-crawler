using System;
using System.Linq;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

using KrakenMPSPBusiness.Models;
using KrakenMPSPBusiness.Repository;

namespace KrakenMPSPServer.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class LegalPersonController : ControllerBase
    {
        private readonly LegalPersonRepository _repository;

        public LegalPersonController()
        {
            _repository = new LegalPersonRepository();
        }

        // GET: api/LegalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegalPersonModel>>> GetLegalPerson()
        {
            return await _repository.GetAll();
        }

        // GET: api/LegalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalPersonModel>> GetLegalPersonModel(Guid id)
        {
            var legalPersonModel = await _repository.FindById(id);

            if (legalPersonModel == null)
            {
                return NotFound();
            }

            return legalPersonModel;
        }

        // PUT: api/LegalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLegalPersonModel(Guid id, LegalPersonModel legalPersonModel)
        {
            if (id != legalPersonModel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateById(id, legalPersonModel);
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
            _repository.Save(legalPersonModel);

            return CreatedAtAction("GetLegalPersonModel", new { id = legalPersonModel.Id }, legalPersonModel);
        }

        // DELETE: api/LegalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LegalPersonModel>> DeleteLegalPersonModel(Guid id)
        {
            var legalPersonModel = await _repository.FindById(id);
            if (legalPersonModel == null)
            {
                return NotFound();
            }

            _repository.Delete(legalPersonModel);

            return legalPersonModel;
        }

        private bool LegalPersonModelExists(Guid id)
        {
            return _repository.FindById(id).IsCompleted;
        }
    }
}
