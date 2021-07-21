using Microsoft.EntityFrameworkCore;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web.Data
{
    public class Scaleble_Web_Context : DbContext
    {
        public Scaleble_Web_Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Difference> Differences { get; set; }
   
    }
}
