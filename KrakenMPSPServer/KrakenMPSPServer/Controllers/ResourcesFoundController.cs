using System;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using KrakenMPSPBusiness.Models;
using KrakenMPSPBusiness.Repository;

namespace KrakenMPSPServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class ResourcesFoundController : ControllerBase
    {
        private readonly ResourcesFoundRepository _repository;

        public ResourcesFoundController()
        {
            _repository = new ResourcesFoundRepository();
        }

        // GET: api/ResourcesFound
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourcesFoundModel>>> GetAll()
        {
            try
            {
                var values = _repository.GetAll();

                if (values == null)
                {
                    return NotFound();
                }

                return Ok(values);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/ResourcesFound/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourcesFoundModel>> FindById(Guid id)
        {
            try
            {
                var model = _repository.FindById(id);

                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT: api/ResourcesFound/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ResourcesFoundModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateById(id, model);
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!ExistsById(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/ResourcesFound
        [HttpPost]
        public async Task<ActionResult<ResourcesFoundModel>> Create(ResourcesFoundModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var id = _repository.Save(model);

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE: api/ResourcesFound/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResourcesFoundModel>> Remove(Guid id)
        {
            var model = _repository.FindById(id);
            if (model == null)
            {
                return NotFound();
            }

            try
            {
                _repository.Delete(model);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        private bool ExistsById(Guid id)
        {
            var model = _repository.FindById(id);
            return model != null;
        }
    }
}
