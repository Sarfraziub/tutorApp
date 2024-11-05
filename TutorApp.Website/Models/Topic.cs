namespace TutorApp.Website.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<StudentFile> Files { get; set; }
        public Topic()
        {
            Files = new HashSet<StudentFile>();
        }
    }
}
