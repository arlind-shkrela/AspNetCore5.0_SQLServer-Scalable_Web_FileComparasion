using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalable_Web.Data;
using Scalable_Web.DTO.Request;
using Scalable_Web.DTO.Response;
using Scalable_Web.Helpers;
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
                if (ExtensionMethods.AreEqual(file))
                {
                    var fileToReturn = ExtensionMethods.Base64BitsDifferent(file.Left, file.Right);
                    file.Left = fileToReturn;
                    return _mapper.Map<DifferenceResponseDTO>(file);
                }
                else
                {
                    return _mapper.Map<DifferenceResponseDTO>(file);
                }

            }
            return _mapper.Map<DifferenceResponseDTO>(file); ;
        }

        public async Task<int> PostLeftAsync(DifferencePostLeft model)
        {
            Difference diff = new Difference();

            var exist = await GetByIdAsync(model.Id);
            
            if (exist != null)
            {
                exist.Left = model.Left;
                await UpdateAsync(exist);
            }
            else
            {
                diff.Id = model.Id;
                diff.Left = model.Left;
                await _context.Differences.AddAsync(diff);
                await _context.SaveChangesAsync();
                return diff.Id;
            }
            return diff.Id;
        }

        public async Task<int> PostRightAsync(DifferencePostRight model)
        {
            Difference diff = new Difference();
            var exist = await GetByIdAsync(model.Id);

            if (exist != null)
            {
                exist.Right = model.Right;
                await UpdateAsync(exist);
            }
            else
            {
                diff.Id = model.Id;
                diff.Right = model.Right;
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

        public async Task DeleteAsync(Difference model)
        {
            try
            {
                _context.Differences.Remove(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
