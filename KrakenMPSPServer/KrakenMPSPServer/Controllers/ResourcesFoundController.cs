﻿using System;

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
                var values = await _repository.GetAll();

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

        // PUT: api/ResourcesFound/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ResourcesFoundModel model)
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
                var id = await _repository.Save(model);

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
