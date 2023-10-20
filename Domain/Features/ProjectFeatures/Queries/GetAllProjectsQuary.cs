using Core.Dtos.Project;
using Domain.DomainModels.Entities;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetAllProjectsQuary() : IRequest<IEnumerable<ProjectDto>>;
}
