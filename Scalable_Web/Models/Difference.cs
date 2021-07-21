using Scalable_Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Scalable_Web.Models
{
    public class Difference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] Left { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] Right { get; set; }


    }
}
