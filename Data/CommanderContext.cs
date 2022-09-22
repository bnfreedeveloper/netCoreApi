using Microsoft.EntityFrameworkCore;
using netcoreapi.Models;

namespace netcoreapi.Data
{

    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> options) : base(options)
        {

        }
        public DbSet<Command> Commands { get; set; }
    }
}