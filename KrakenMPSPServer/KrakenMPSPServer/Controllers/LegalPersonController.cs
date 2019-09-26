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

        // GET: api/LegalPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalPersonModel>> FindById(Guid id)
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

        // PUT: api/LegalPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LegalPersonModel model)
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
                var id = _repository.Save(model);

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
            LegalPersonModel model = _repository.FindById(id);
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
