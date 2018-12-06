using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.DB
{
    public interface IWebService
    {
        Task<List<Model.ToDo>> GetToDoListAsync();

        Task SaveToDoListAsync(List<Model.ToDo> toDoList);

        Task DeleteToDoListAsync();
    }
}
