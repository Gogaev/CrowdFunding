﻿using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CreateProjectCommandHandler(IApplicationDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project()
            {
                Id = Guid.NewGuid().ToString(),
                Status = Status.Draft,
                CreatorId = _userService.GetUserId(),
                InvestedMoney = 0
            };

            project = _mapper.Map(request, project);
            
            var result = _context.Projects.Add(project);

            if (result is null)
            {
                throw new Exception("Can't create project");
            }

            await _context.SaveChanges();

        }
    }
}
