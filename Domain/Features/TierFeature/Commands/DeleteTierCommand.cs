using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
{
    public class DeleteTierCommand : IRequest<int>
    {
        public int Id { get; set; }
        
    }
}
