using System.ComponentModel.DataAnnotations.Schema;

namespace TutorApp.Website.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [NotMapped]
        public string ConfirmPassword{ get; set; }
        public bool IsApproved { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
