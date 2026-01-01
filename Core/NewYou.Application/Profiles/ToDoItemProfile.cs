using AutoMapper;
using NewYou.Application.Features.Commands.ToDoItem.CreateToDo;
using NewYou.Domain.Entities;

namespace NewYou.Application.Mappings;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<CreateToDoItemRequest, ToDoItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreateData, opt => opt.MapFrom(src => DateTime.UtcNow));

    }
}