using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project == null)
            {
                return new Response { Status = ResponseStatus.NotFound, Message = "Project doesn't exist!" };
            }
            project = _mapper.Map(request, project);

            await _context.SaveChanges();

            return new Response { Status = ResponseStatus.Success, Message = "Project was updated successfully" };
        }
    }
}
