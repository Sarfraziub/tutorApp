using System.ComponentModel.DataAnnotations;

namespace TutorApp.Website.Models
{
    public class FileModel
    {
        public int SrNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class FileUploadModel
    {
        public string FileName { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
