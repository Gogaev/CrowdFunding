﻿using Core.Dtos.Project;
using Domain.DomainModels.Entities;
using MediatR;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetProjectByIdQuery(int Id) : IRequest<ProjectWithTiersDto>;
}
