using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Scalable_Web.Controllers
{
    [ApiController]
    public class DifferenceController : ControllerBase
    {
        private readonly IDifference _dataRepository;

        private readonly ILogger<DifferenceController> _logger;

        public DifferenceController(IDifference dataRepository, ILogger<DifferenceController> logger)
        {
            _dataRepository = dataRepository;
            _logger = logger;

        }


        [HttpGet("v1/diff/{ID}")]
        [ActionName(nameof(Get))]
        public async Task<ActionResult<DifferenceResponseDTO>> Get(int ID)
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
        public async Task<ActionResult<Difference>> PostLeftAsync(int ID, [FromBody] byte[] modelLeft)
        {
            if (modelLeft == null)
            {
                return BadRequest("base64 data is null.");
            }
            await _dataRepository.PostLeftAsync(ID, modelLeft);
            return CreatedAtAction(nameof(Get), new { id = ID });

        }

        [HttpPost]
        [ActionName(nameof(PostRightAsync))]
        [Route("v1/diff/{ID}/right")]
        public async Task<ActionResult<Difference>> PostRightAsync(int ID,[FromBody] byte[] modelRight)
        {
            if (modelRight == null)
            {
                return BadRequest("base64 data is null.");
            }
            await _dataRepository.PostRightAsync(ID,modelRight);
            return CreatedAtAction(nameof(Get), new { id = ID });

        }


    }
}
