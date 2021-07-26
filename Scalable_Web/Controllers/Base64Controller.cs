using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scalable_Web.DTO.Response;
using Scalable_Web.Interfaces;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Base64Controller : ControllerBase
    {
        private readonly IBase64 _dataRepository;

        public Base64Controller(IBase64 dataRepository)
        {
            _dataRepository = dataRepository;
        }


        [HttpPost]
        [Route("~/Encode")]
        [ActionName(nameof(Encode))]
        public async Task<ActionResult<Base64ResponseDTO>> Encode(EncodeBase64 base64)
        {
            if (base64 == null)
            {
                return BadRequest("base64 data is null.");
            }
            var data = await _dataRepository.EncodeBase64(base64);
            return Ok(data);

        }

        [HttpPost]
        [Route("~/Decode")]
        [ActionName(nameof(Decode))]
        public async Task<ActionResult<Base64ResponseDTO>> Decode(DecodeBase64 base64)
        {
            if (base64 == null)
            {
                return BadRequest("base64 data is null.");
            }
            var data = await _dataRepository.DecodeBase64(base64);
            return Ok(data);

        }

    }
}
