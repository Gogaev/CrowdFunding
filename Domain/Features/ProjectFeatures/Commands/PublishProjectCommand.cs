using Domain.DomainModels.Enums;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record PublishProjectCommand(string Id) : IRequest;
}
