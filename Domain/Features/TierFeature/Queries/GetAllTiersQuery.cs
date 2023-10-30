﻿using AutoMapper;
using Core.Dtos.Tier;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Queries
{
    public record GetAllTiersQuery : IRequest<IEnumerable<TierDto>?>
    {
        public class GetAllTiersQueryHandler : IRequestHandler<GetAllTiersQuery, IEnumerable<TierDto>?>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetAllTiersQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<TierDto>?> Handle(GetAllTiersQuery request, CancellationToken cancellationToken)
            {
                return _mapper.Map<List<Tier>, List<TierDto>>(
                    await _context.Tiers.ToListAsync(cancellationToken: cancellationToken)
                );
            }
        }
    }
}