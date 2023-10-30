using AutoMapper;
using Core.Dtos.User;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.UserFeatures.Queries
{
    public record GetUserByIdQuery(string Id) : IRequest<UserDto>
    {
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public GetUserByIdQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (user is null)
                {
                    throw new KeyNotFoundException("User with such id doesn't exist");
                }

                return _mapper.Map<UserDto>(user);
            }
        }
    }
}
