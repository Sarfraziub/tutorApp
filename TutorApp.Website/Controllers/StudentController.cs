using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorApp.Website.Context;
using TutorApp.Website.Models;
using TutorApp.Website.ViewModels;

namespace TutorApp.Website.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Dashboard()
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == User.Identity.Name);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var studentClass = await _context.Students
                .Include(s=>s.Class)
                    .ThenInclude(s=>s.Subjects)
                    .ThenInclude(sub=>sub.Topics)
                    //.ThenInclude(sub=>sub.Files)
                    .FirstOrDefaultAsync(s=>s.StudentId == student.StudentId);
           
            return View(studentClass);
        }


        public async Task<IActionResult> ViewTopic(int topicId)
        {
            var studentClass = await _context.Students
                .AsNoTracking() 
                .Include(s => s.Class)
                .ThenInclude(c => c.Subjects)
                .ThenInclude(sub => sub.Topics) 
                .FirstOrDefaultAsync(s => s.Email == User.Identity.Name);

            if (studentClass == null)
            {
                return NotFound("Student not found.");
            }

            // Fetch the topic and its associated files using topicId
            var topic = _context.Topics
                .Include(t => t.Files)
                .FirstOrDefault(t => t.TopicId == topicId);

            if (topic == null)
            {
                return NotFound("Topic not found.");
            }

            // Pass both the topic and subjects back to the view
            var viewModel = new TopicDetailsViewModel
            {
                StudentSubjects = studentClass.Class.Subjects,
                SelectedTopic = topic
            };

            return View(viewModel);
        }


        public FileResult DownloadFile(int fileId)
        {
            var file = _context.Files.Find(fileId);
            return File(file.FilePath, "application/octet-stream", file.FileName);
        }
    }
}
