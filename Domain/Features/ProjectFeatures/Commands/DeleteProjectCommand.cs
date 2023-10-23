using Core.Dtos;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record DeleteProjectCommand(string Id) : IRequest<Response>;
}
