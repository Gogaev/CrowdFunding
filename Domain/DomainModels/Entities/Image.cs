namespace Domain.DomainModels.Entities;

public class Image
{
    public Guid Id { get; set; }
    public byte[] ImageData { get; set; }
}