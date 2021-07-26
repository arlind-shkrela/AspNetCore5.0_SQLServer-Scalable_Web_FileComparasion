using Scalable_Web.Data;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scalable_Web.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {

        }
        public void Seed(Scaleble_Web_Context context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Differences.AddRange(
                new Difference() { Id = 1, Left = CreateSpecialByteArray("Hello there"), Right = CreateSpecialByteArray("Hello there") },
                new Difference() { Id = 2, Left = CreateSpecialByteArray("Hello there v1"), Right = CreateSpecialByteArray("Hello there") },
                new Difference() { Id = 3, Left = CreateSpecialByteArray("Hello  there"), Right = CreateSpecialByteArray("Hello there") },
                new Difference() { Id = 4, Left = CreateSpecialByteArray("Hello there"), Right = CreateSpecialByteArray("Hello therW") }
            );

            context.SaveChanges();
        }
        public static byte[] CreateSpecialByteArray(string text)
        {
            return Encoding.ASCII.GetBytes(text); ;
        }


    }
}
