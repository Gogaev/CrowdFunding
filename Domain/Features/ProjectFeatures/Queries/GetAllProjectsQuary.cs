using Domain.DomainModels;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetAllProjectsQuary() : IRequest<IEnumerable<Project>>;
}
