using Scalable_Web.DTO.Request;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web
{
    public interface IDifference
    {
        Task<Difference> GetByIdAsync(int id);
        Task<DifferenceResponseDTO> GetByIdAsyncAndCompare(int id);

        Task<int> PostLeftAsync(DifferencePostLeft modelLeft);
        Task<int> PostRightAsync(DifferencePostRight modelRight); //int id, byte[] right

        Task UpdateAsync(Difference model);

        Task DeleteAsync(Difference model);

    }
}
