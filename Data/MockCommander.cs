using System.Collections.Generic;
using System.Threading.Tasks;
using netcoreapi.Models;

namespace netcoreapi.Data
{
    public class MockCommander : IRepo

    {
        public Task CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Command>> getAll()
        {
            IEnumerable<Command> result = new List<Command>(){
                new Command{
                Id= 0,
                HowTo ="faire la vaiselle",
                Line="frotter",
                Platform ="la cuisine"
            },
            new Command{
                Id= 1,
                HowTo ="faire le ménage",
                Line="aspirateur et serpillère",
                Platform ="la maison"
            },
            new Command{
                Id= 2,
                HowTo ="faire du sport",
                Line="no pain no gain",
                Platform ="whaterver"
            }
            };
            return Task.FromResult(result);
        }

        public Task<Command> GetCommandById(int id)
        {
            return Task.FromResult(new Command
            {
                Id = 0,
                HowTo = "faire la vaiselle",
                Line = "frotter",
                Platform = "la cuisine"
            });
        }

        public Task UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IRepo.SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}