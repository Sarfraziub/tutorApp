namespace TutorApp.Website.Models
{
    
    public class Subject
    {
        public Subject()
        {
            Topics = new HashSet<Topic>();
        }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }

}
