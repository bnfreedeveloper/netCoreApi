using System.Collections.Generic;
using System.Threading.Tasks;
using netcoreapi.Models;

namespace netcoreapi.Data
{

    public interface IRepo
    {
        Task<IEnumerable<Command>> getAll();
        Task<Command> GetCommandById(int id);
        Task CreateCommand(Command cmd);

        Task<bool> SaveChanges();

        Task UpdateCommand(Command cmd);

        Task DeleteCommand(Command cmd);

    }
}