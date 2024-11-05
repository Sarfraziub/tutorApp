namespace TutorApp.Website.Models
{
    public class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        // Navigation property for the one-to-many relationship
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }

}
