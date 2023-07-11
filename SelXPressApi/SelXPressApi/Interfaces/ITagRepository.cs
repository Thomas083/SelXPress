using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ITagRepository
    {
        ICollection<Tag> GetAllTags();
    }
}
