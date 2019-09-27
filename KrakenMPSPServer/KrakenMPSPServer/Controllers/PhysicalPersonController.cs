using System;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using KrakenMPSPBusiness.Models;

using KrakenMPSPServer.Repository;

namespace KrakenMPSPServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class PhysicalPersonController : ControllerBase
    {
        private readonly PhysicalPersonRepository _repository;

        public PhysicalPersonController()
        {
            _repository = new PhysicalPersonRepository();
        }

        // GET: api/PhysicalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalPersonModel>>> GetAll()
        {
            try
            {
                var values = await _repository.GetAll();

                return Ok(values);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/PhysicalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> FindById(Guid id)
        {
            try
            {
                var model = await _repository.FindById(id);

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

        // PUT: api/PhysicalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PhysicalPersonModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateById(id, model);
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!await ExistsById(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/PhysicalPerson
        [HttpPost]
        public async Task<ActionResult<PhysicalPersonModel>> Create(PhysicalPersonModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var id = await _repository.Save(model);

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE: api/PhysicalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhysicalPersonModel>> Remove(Guid id)
        {
            var model = await _repository.FindById(id);
            if (model == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.Delete(model.Id);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        private async Task<bool> ExistsById(Guid id)
        {
            var model = await _repository.FindById(id);
            return model != null;
        }
    }
}
