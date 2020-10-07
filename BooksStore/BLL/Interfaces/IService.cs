using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.BLL.Interfaces
{
    public interface IService<TDto> 
        where TDto:class
    {
        Task<bool> ExistsAsync(TDto entity);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetSingleAsync(int id);
    }
}
