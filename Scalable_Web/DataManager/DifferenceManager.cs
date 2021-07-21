using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalable_Web.Data;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web.DataManager
{
    public class DifferenceManager : IDifference
    {
        readonly Scaleble_Web_Context _context;
        private readonly IMapper _mapper;

        public DifferenceManager(Scaleble_Web_Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Difference> GetByIdAsync(int id)
        {
            return await _context.Differences.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<DifferenceResponseDTO> GetByIdAsyncAndCompare(int id)
        {
            var file = await GetByIdAsync(id);
            if (file != null)
            {

            }
            DifferenceResponseDTO difference = _mapper.Map<DifferenceResponseDTO>(file);
            return difference;
        }

        public async Task<int> PostLeftAsync(int id, byte[] left)
        {
            Difference diff = new Difference();

            var exist = await GetByIdAsync(id);
            
            if (exist != null)
            {
                exist.Left = left;
                await UpdateAsync(exist);
            }
            else
            {
                diff.Id = id;
                diff.Left = left;
                await _context.Differences.AddAsync(diff);
                await _context.SaveChangesAsync();
                return diff.Id;
            }
            return diff.Id;
        }

        public async Task<int> PostRightAsync(int id, byte[] right)
        {
            Difference diff = new Difference();
            var exist = await GetByIdAsync(id);

            if (exist != null)
            {
                exist.Right = right;
                await UpdateAsync(exist);
            }
            else
            {
                diff.Id = id;
                diff.Right = right;
                await _context.Differences.AddAsync(diff);
                await _context.SaveChangesAsync();
                return diff.Id;
            }
            return diff.Id;
        }

        public async Task UpdateAsync(Difference model)
        {
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
