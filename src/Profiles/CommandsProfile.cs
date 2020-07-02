using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    // Parent Profile comes from the Automapper package
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Mapping: Source -> target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
        }
    }
}