using AutoMapper;
using Core.Dtos.User;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.UserFeatures.Queries
{
    public record GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public GetUsersQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                return _mapper.Map<List<ApplicationUser>, List<UserDto>>(await _userManager.Users
                    .Include(x => x.CreatedProjects)
                    .ToListAsync(cancellationToken: cancellationToken));
            }
        }
    }
}