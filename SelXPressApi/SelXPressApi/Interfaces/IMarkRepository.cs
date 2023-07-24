using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IMarkRepository
    {
        Task<List<Mark>> GetAllMark();
        Task<Mark> GetMarkById(int id);
        Task<List<Mark>> GetMarkByUser(int id);
        Task<List<Mark>> GetMarkByProduct(int id);
        Task<bool> CreateMark(Mark mark);
        Task<bool> UpdateMarkById(UpdateMarkDTO updateMark, int id);
        Task<bool> DeleteMarkById(int id);
        Task<bool> MarkExists(int id);
    }
}
