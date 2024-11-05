namespace TutorApp.Website.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ClassId { get; set; } // Assuming students are assigned to a class at registration
    }

}
