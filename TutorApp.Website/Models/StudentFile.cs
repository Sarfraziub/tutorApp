using System.ComponentModel.DataAnnotations;

namespace TutorApp.Website.Models
{
    public class StudentFile
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
