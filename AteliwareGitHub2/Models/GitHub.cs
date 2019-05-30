using System;
using System.ComponentModel.DataAnnotations;

namespace AteliwareGitHub2.Models
{
    public partial class GitHub
    {
        [Key]
        public int Identificador { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Description { get; set; }
        public String HasDownloads { get; set; }
        public String HasIssues { get; set; }
        public String HtmlUrl { get; set; }
        public String Language { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StargazersCount { get; set; }
        public String Name { get; set; }
        public String Owner { get; set; }
    }
}