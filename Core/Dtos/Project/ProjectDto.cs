﻿
namespace Core.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal InvestedMoney { get; set; }
        public DateTime LastDay { get; set; }
        public string? CreatorName { get; set; }
    }
}
