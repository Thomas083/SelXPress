using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllTags();
        Task<Tag> GetTagById(int id);
        Task<bool> TagExists(int id);
        Task<bool> CreateTag(CreateTagDTO createTag);
        Task<bool> UpdateTag(int id, UpdateTagDTO updateTag);
        Task<bool> DeleteTag(int id);
    }
}
