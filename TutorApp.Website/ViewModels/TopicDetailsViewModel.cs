using TutorApp.Website.Models;

namespace TutorApp.Website.ViewModels
{
    public class TopicDetailsViewModel
    {
        public IEnumerable<Subject> StudentSubjects { get; set; }
        public Topic SelectedTopic { get; set; }
    }

}
