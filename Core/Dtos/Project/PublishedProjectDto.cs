namespace Core.Dtos.Project
{
    public class PublishedProjectDto
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal InvestedMoney { get; set; }
        public DateTime LastDay { get; set; }
        public string? CreatorName { get; set; }
        public List<string>? Supporters { get; set; }
    }
}
