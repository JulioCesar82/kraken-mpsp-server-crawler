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
    public class ResourcesFoundController : ControllerBase
    {
        private readonly ResourcesFoundRepository _repository;

        public ResourcesFoundController()
        {
            _repository = new ResourcesFoundRepository();
        }

        // GET: api/ResourcesFound
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourcesFoundModel>>> GetResourcesFound()
        {
            return await _repository.GetAll();
        }

        // GET: api/ResourcesFound/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourcesFoundModel>> GetResourcesFoundModel(Guid id)
        {
            var resourcesFoundModel = await _repository.FindById(id);

            if (resourcesFoundModel == null)
            {
                return NotFound();
            }

            return resourcesFoundModel;
        }

        // PUT: api/ResourcesFound/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResourcesFoundModel(Guid id, ResourcesFoundModel resourcesFoundModel)
        {
            if (id != resourcesFoundModel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateById(id, resourcesFoundModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourcesFoundModelExists(id))
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

        // POST: api/ResourcesFound
        [HttpPost]
        public async Task<ActionResult<ResourcesFoundModel>> PostResourcesFoundModel(ResourcesFoundModel resourcesFoundModel)
        {
            _repository.Save(resourcesFoundModel);

            return CreatedAtAction("GetResourcesFoundModel", new { id = resourcesFoundModel.Id }, resourcesFoundModel);
        }

        // DELETE: api/ResourcesFound/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResourcesFoundModel>> DeleteResourcesFoundModel(Guid id)
        {
            var resourcesFoundModel = await _repository.FindById(id);
            if (resourcesFoundModel == null)
            {
                return NotFound();
            }

            _repository.Delete(resourcesFoundModel);

            return resourcesFoundModel;
        }

        private bool ResourcesFoundModelExists(Guid id)
        {
            return _repository.FindById(id).IsCompleted;
        }
    }
}
