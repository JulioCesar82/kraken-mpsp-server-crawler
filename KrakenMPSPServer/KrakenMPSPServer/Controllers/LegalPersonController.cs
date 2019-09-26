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
    public class LegalPersonController : ControllerBase
    {
        private readonly LegalPersonRepository _repository;

        public LegalPersonController()
        {
            _repository = new LegalPersonRepository();
        }

        // GET: api/LegalPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegalPersonModel>>> GetAll()
        {
            try
            {
                var values = await _repository.GetAll();

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

        // GET: api/LegalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalPersonModel>> FindById(Guid id)
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

        // PUT: api/LegalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LegalPersonModel model)
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

        // POST: api/LegalPerson
        [HttpPost]
        public async Task<ActionResult<LegalPersonModel>> Create(LegalPersonModel model)
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

        // DELETE: api/LegalPerson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LegalPersonModel>> Remove(Guid id)
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
