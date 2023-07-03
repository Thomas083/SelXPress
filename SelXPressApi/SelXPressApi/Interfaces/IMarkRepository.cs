using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IMarkRepository
    {
        ICollection<Mark> GetAllMarks();
    }
}
