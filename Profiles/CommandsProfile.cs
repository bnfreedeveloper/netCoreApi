using AutoMapper;
using netcoreapi.Models;
using netcoreapi.Dtos;

namespace netcoreapi.CommandProfile
{

    public class CommandProfile : Profile
    {

        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>().ReverseMap();

        }
    }
}