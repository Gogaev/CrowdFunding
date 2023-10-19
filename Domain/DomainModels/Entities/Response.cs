using Domain.DomainModels.Enums;

namespace Core.Dtos
{
    public class Response
    {
        public ResponseStatus? Status { get; set; }
        public string? Message { get; set; }
    }
}
