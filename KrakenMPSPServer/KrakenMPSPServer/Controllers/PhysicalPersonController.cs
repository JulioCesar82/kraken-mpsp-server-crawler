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
    public class PhysicalPersonController : ControllerBase
    {
        private readonly PhysicalPersonRepository _repository;

        public PhysicalPersonController()
        {
            _repository = new PhysicalPersonRepository();
        }

        // GET: api/PhysicalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalPersonModel>>> GetPhysicalPerson()
        {
            return await _repository.GetAll();
        }

        // GET: api/PhysicalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> GetPhysicalPersonModel(Guid id)
        {
            var physicalPersonModel = await _repository.FindById(id);

            if (physicalPersonModel == null)
            {
                return NotFound();
            }

            return physicalPersonModel;
        }

        // PUT: api/PhysicalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhysicalPersonModel(Guid id, PhysicalPersonModel physicalPersonModel)
        {
            if (id != physicalPersonModel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateById(id, physicalPersonModel);
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
            _repository.Save(physicalPersonModel);

            return CreatedAtAction("GetPhysicalPersonModel", new { id = physicalPersonModel.Id }, physicalPersonModel);
        }

        // DELETE: api/PhysicalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> DeletePhysicalPersonModel(Guid id)
        {
            var physicalPersonModel = await _repository.FindById(id);
            if (physicalPersonModel == null)
            {
                return NotFound();
            }

            _repository.Delete(physicalPersonModel);

            return physicalPersonModel;
        }

        private bool PhysicalPersonModelExists(Guid id)
        {
            return _repository.FindById(id).IsCompleted;
        }
    }
}
