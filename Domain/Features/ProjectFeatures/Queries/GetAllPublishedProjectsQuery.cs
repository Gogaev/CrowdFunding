using Core.Dtos.Project;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetAllPublishedProjectsQuery() : IRequest<IEnumerable<PublishedProjectDto>>;
}
