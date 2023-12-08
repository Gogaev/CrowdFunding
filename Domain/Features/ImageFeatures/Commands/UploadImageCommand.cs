using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Features.ImageFeatures.Commands;

public record UploadImageCommand(IFormFile file) : IRequest<Guid>
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UploadImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.file.CopyToAsync(memoryStream);
                var image = new Image
                {
                    Id = new Guid(),
                    ImageData = memoryStream.ToArray()
                };

                _context.Images.Add(image);
                await _context.SaveChanges();

                return (image.Id);
            }
        }
    }
}