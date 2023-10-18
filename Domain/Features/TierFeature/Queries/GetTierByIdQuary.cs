using AutoMapper;
using Core.Dtos.Tier;
using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Queries
{
    public record GetTierByIdQuary(int Id) : IRequest<TierDto?>;
}
