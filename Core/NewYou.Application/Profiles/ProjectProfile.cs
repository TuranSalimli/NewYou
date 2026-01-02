using AutoMapper;
using NewYou.Application.DTOs.ProjectDTOs;
using NewYou.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYou.Application.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>()
             .ForMember(dest => dest.ToDoItemsCount,
                        opt => opt.MapFrom(src => src.ToDoItems != null ? src.ToDoItems.Count : 0));

            CreateMap<CreateProjectRequest, Project>();
        }
    }
}
