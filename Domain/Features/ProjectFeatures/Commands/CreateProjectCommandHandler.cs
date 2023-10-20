using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Response>
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

        public async Task<Response> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project()
            {
                Status = Status.Draft,
                CreatorId = _userService.GetUserId(),
                InvestedMoney = 0
            };

            project = _mapper.Map(request, project);
            
            _context.Projects.Add(project);
            await _context.SaveChanges();

            return new Response { Status = ResponseStatus.Success, Message = "Project was created successfully" };
        }
    }
}
