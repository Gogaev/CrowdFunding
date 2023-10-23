﻿using Core.Dtos.Project;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetProjectByIdQuery(string Id) : IRequest<ProjectWithTiersDto>;
}
