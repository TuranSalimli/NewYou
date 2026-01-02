using MediatR;
using NewYou.Application.DTOs;
using NewYou.Application.DTOs.ProjectDTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.Project.GetAllProjects;

public class GetAllProjectsRequest : IRequest<Response<IList<ProjectDTO>>> {}