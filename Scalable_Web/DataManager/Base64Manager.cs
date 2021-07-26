using Scalable_Web.DTO.Response;
using Scalable_Web.Interfaces;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scalable_Web.DataManager
{
    public class Base64Manager : IBase64
    {
        public async Task<Base64ResponseDTO> EncodeBase64(EncodeBase64 base64)
        {
            Base64ResponseDTO _base64 = new Base64ResponseDTO();
            var valueBytes = Encoding.UTF8.GetBytes(base64.File);
            _base64.File = Convert.ToBase64String(valueBytes); ;
            return _base64;
        }

        public async Task<Base64ResponseDTO> DecodeBase64(DecodeBase64 base64)
        {
            Base64ResponseDTO _base64 = new Base64ResponseDTO();
            _base64.File = System.Text.Encoding.UTF8.GetString(base64.File);
            return _base64;
        }

    }
}
