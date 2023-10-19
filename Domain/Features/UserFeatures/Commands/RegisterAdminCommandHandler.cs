using AutoMapper;
using Core.Dtos;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Domain.Features.UserFeatures.Commands
{
    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public RegisterAdminCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

        public Task<Response> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            if(request.AdminKey != _config)
            {

            }
        }
    }
}
