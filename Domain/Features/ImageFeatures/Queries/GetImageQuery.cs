using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ImageFeatures.Queries;

public record GetImageQuery(string id) : IRequest<Image>
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, Image>
    {
        private readonly IApplicationDbContext _context;

        public GetImageQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Image> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var image = await _context.Images.FirstOrDefaultAsync(x => x.Id.ToString() == request.id);
            
            if (image == null)
            {
                throw new NotFoundException("Image with such id doesn't exist");
            }

            return image;
        }
    }
}