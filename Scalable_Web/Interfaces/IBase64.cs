using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web.Interfaces
{
    public interface IBase64
    {
        Task<Base64ResponseDTO> EncodeBase64(EncodeBase64 text);
        Task<Base64ResponseDTO> DecodeBase64(DecodeBase64 text);

    }
}
