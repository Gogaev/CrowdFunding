using Domain.DomainModels;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetProjectByIdQuery(int Id) : IRequest<Project>;
}
