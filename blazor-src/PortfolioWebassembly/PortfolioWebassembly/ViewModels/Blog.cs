﻿namespace PortfolioWebassembly.ViewModels
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
    }
}
