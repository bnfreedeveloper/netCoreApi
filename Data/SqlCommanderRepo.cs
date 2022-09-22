using System.Collections.Generic;
using netcoreapi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace netcoreapi.Data
{

    public class SqlCommanderRepo : IRepo
    {

        private readonly CommanderContext _db;
        public SqlCommanderRepo(CommanderContext db)
        {
            _db = db;
        }

        public async Task CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            await this._db.Commands.AddAsync(cmd);

        }

        public async Task DeleteCommand(Command cmd)
        {
            _db.Remove(cmd);
        }

        public async Task<IEnumerable<Command>> getAll()
        {
            return await _db.Commands.ToListAsync();
        }

        public async Task<Command> GetCommandById(int id)
        {
            return await _db.Commands.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            var result = await this._db.SaveChangesAsync();
            return result >= 0 ? true : false;
        }

        public async Task UpdateCommand(Command cmd)
        {
            //_db.Commands.Update(cmd);
        }
    }
}