using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Scalable_Web.DTO.Request;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Scalable_Web.Controllers
{
    [ApiController]
    public class DifferenceController : ControllerBase // test
    {
        private readonly IDifference _dataRepository;

        public DifferenceController(IDifference dataRepository)
        {
            _dataRepository = dataRepository;
        }


        [HttpGet("v1/diff/{ID}")]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Get(int ID)
        {
            if (ID == 0)
            {
                return BadRequest();
            }

            var difference = await _dataRepository.GetByIdAsyncAndCompare(ID);
            if (difference == null)
            {
                return NotFound("Record couldn't be found.");
            }
            return Ok(difference);
        }

        [HttpPost]
        [Route("v1/diff/{ID}/left")]
        [ActionName(nameof(PostLeftAsync))]
        public async Task<IActionResult> PostLeftAsync(DifferencePostLeft modelLeft)
        {
            if (modelLeft == null)
            {
                return BadRequest("base64 data is null.");
            }
            await _dataRepository.PostLeftAsync(modelLeft);
            return CreatedAtAction(nameof(Get), new { id = modelLeft.Id }, modelLeft);

        }

        [HttpPost]
        [ActionName(nameof(PostRightAsync))]
        [Route("v1/diff/{ID}/right")]
        public async Task<IActionResult> PostRightAsync(DifferencePostRight modelRight)
        {
            if (modelRight == null)
            {
                return BadRequest("base64 data is null.");
            }
            await _dataRepository.PostRightAsync(modelRight);
            return CreatedAtAction(nameof(Get), new { id = modelRight.Id },modelRight);

        }

        [HttpDelete("v1/diff/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var data = await _dataRepository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound("The record couldn't be found.");
            }

            try
            {
                await _dataRepository.DeleteAsync(data);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }

        }

    }


}
