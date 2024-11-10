using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TutorApp.Website.ViewModels;
using TutorApp.Website.Context;
using TutorApp.Website.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NuGet.Versioning;

namespace TutorApp.Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly SchoolDbContext _context;

        private readonly PasswordHasher<Student> _passwordHasher;
        public AdminController(SchoolDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Student>();
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == User.Identity.Name);
            return View();
        }
        [Authorize]
        public IActionResult Classes()
        {
            var classes = _context.Classes.ToList();
            return View(classes);
        }
        [Authorize]
        public IActionResult Students()
        {
            var students = _context.Students.ToList();
            return View(students);
        }
        [Authorize]
        public IActionResult Subjects(int classId)
        {
            var subjects = _context.Subjects.Where(x=>x.ClassId == classId).ToList();
            ViewBag.ClassName = _context.Classes.Where(x => x.ClassId == classId).FirstOrDefault().ClassName;
            ViewBag.ClassId = classId;
            return View(subjects);
        }
        [Authorize]
        public IActionResult AddSubject(int classId)
        {
            Subject subject = new Subject();
            subject.ClassId = classId;
            return View(subject);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddSubject(Subject subject)
        {
            if (subject != null && !string.IsNullOrEmpty(subject.SubjectName))
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return RedirectToAction("Subjects",new { classId=subject.ClassId});
            }
            return View(subject);
        }
        [Authorize]
        public IActionResult AddStudent()
        {
            Student student = new Student();
            ViewBag.Students = _context.Students.ToList();

            return View(student);
        }
        [Authorize]
        public IActionResult EditStudent(int studentId)
        {
            ViewBag.Students = _context.Students.ToList();
            Student student = new Student();
            student = _context.Students.Where(x=>x.StudentId == studentId).FirstOrDefault();
            return View("AddStudent", student);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if(student.StudentId==0&&(student.ConfirmPassword != student.PasswordHash || string.IsNullOrEmpty(student.ConfirmPassword)))
            {
                ViewBag.Students = _context.Students.ToList();
                ViewBag.errorMsg = "passord & confirm password are mismatch!";
                return View(student);
            }
            if(student!=null && !string.IsNullOrEmpty(student.Name) && !string.IsNullOrEmpty(student.Email) && student.ClassId>0)
            {
                if (student.StudentId == 0)
                {
                    student.IsApproved = true;
                    student.PasswordHash = _passwordHasher.HashPassword(student, student.PasswordHash);
                        _context.Students.Add(student);
                }
                else {
                        _context.Students.Update(student);
                }
                _context.SaveChanges();
                return RedirectToAction("Students");
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.errorMsg = "please fill all fields!";
            return View(student);
        }
        [Authorize]
        public IActionResult AddClass()
        {
            Class _class = new Class();
            return View(_class);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddClass(Class _class)
        {
            if (_class != null && !string.IsNullOrEmpty(_class.ClassName))
            {
                _context.Classes.Add(_class);
                _context.SaveChanges();
                return RedirectToAction("Classes");
            }
            return View(_class);
        }

        [Authorize]
        public IActionResult Topics(int subjectId)
        {
            var subjects = _context.Topics.Where(x => x.SubjectId== subjectId).ToList();
            ViewBag.SubjectName = _context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault().SubjectName;
            ViewBag.SubjectId = subjectId;
            ViewBag.ClassId = _context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault().ClassId;
            return View(subjects);
        }
        [Authorize]
        public IActionResult AddTopic(int subjectId)
        {
            Topic topic = new Topic();
            topic.SubjectId = subjectId;
            return View(topic);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddTopic(Topic topic)
        {
            if (topic != null && !string.IsNullOrEmpty(topic.TopicName))
            {
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return RedirectToAction("Topics", new { subjectId = topic.SubjectId });
            }
            return View(topic);
        }
        [Authorize]
        public IActionResult TopicFiles(int topicId)
        {
            Topic topic = _context.Topics.Where(x => x.TopicId == topicId).FirstOrDefault();
            int subjectId = topic.SubjectId;
            ViewBag.TopicName = topic.TopicName;
            Subject subject = _context.Subjects.Where(x => x.SubjectId== topic.SubjectId).FirstOrDefault();
            ViewBag.SubjectName = subject.SubjectName;
            int classId = subject.ClassId;
            string dirPath = Path.Combine("wwwroot", "classes", classId.ToString(), subjectId.ToString(), topicId.ToString());
            List<FileModel> files = new List<FileModel>();
            int srNo = 1;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            foreach (string fileName in Directory.GetFiles(dirPath))
            {
                files.Add(new FileModel
                {
                    SrNo = srNo++,
                    FileName = Path.GetFileName(fileName),
                    FilePath = "/classes/" + classId + "/" + subjectId + "/" + topicId + "/" + Path.GetFileName(fileName)
                });
            }

            ViewBag.ClassId = classId;
            ViewBag.SubjectId = subjectId;
            ViewBag.TopicId = topicId;
            return View(files);
        }
        [Authorize]
        public IActionResult AddTopicFile(int topicId)
        {
            FileUploadModel uploadModel = new FileUploadModel();
            uploadModel.TopicId = topicId;
            Topic topic = _context.Topics.Where(x => x.TopicId == topicId).FirstOrDefault();
            uploadModel.SubjectId = topic.SubjectId;
            ViewBag.TopicName = topic.TopicName;
            ViewBag.SubjectName = _context.Subjects.Where(x => x.SubjectId == topic.SubjectId).FirstOrDefault().SubjectName;
            uploadModel.ClassId = _context.Subjects.Where(x => x.SubjectId == topic.SubjectId).FirstOrDefault().ClassId;
            return View(uploadModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddTopicFile(FileUploadModel model)
        {
            string dirPath = Path.Combine("wwwroot", "classes", model.ClassId.ToString(), model.SubjectId.ToString(), model.TopicId.ToString());
            string fileName = model.FileName + Path.GetExtension(model.File.FileName);
            string filePath = Path.Combine(dirPath, fileName);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (System.IO.File.Exists(filePath))
            {
                ModelState.AddModelError("FileName", "File already exists.");
                return View(model);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }

            return RedirectToAction("TopicFiles", new { classId = model.ClassId, subjectId = model.SubjectId, topicId = model.TopicId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteTopicFile(int topicId, string fileName)
        {
            string dirPath = Path.Combine("wwwroot", "classes", _context.Subjects.Where(x => x.SubjectId == _context.Topics.Where(t => t.TopicId == topicId).FirstOrDefault().SubjectId).FirstOrDefault().ClassId.ToString(),
                _context.Topics.Where(t => t.TopicId == topicId).FirstOrDefault().SubjectId.ToString(), topicId.ToString());

            string filePath = Path.Combine(dirPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Json(new { success = true, message = "File deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "File not found" });
            }
        }
        [HttpPost]
        public IActionResult DeleteStudent(int studentId)
        {
            var student = _context.Students
                .Include(x=>x.Class)
                  .ThenInclude(x=>x.Subjects)
                    .ThenInclude(x=>x.Topics).
                Include(x=>x.Class)
                 .ThenInclude(x => x.Students).
                FirstOrDefault(x=>x.StudentId == studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return Json(new { success = true, message = "Student deleted successfully" });
            }
            return Json(new { success = false, message = "Student not found" });
        }
        [HttpPost]
        public IActionResult DeleteClass(int classId)
        {
            var @class = _context.Classes
                .Include(x=>x.Subjects)
                    .ThenInclude(x=>x.Topics)
                .Include(x=>x.Students).FirstOrDefault(x=>x.ClassId == classId);
            if (@class != null)
            {
                foreach(var sub in @class.Subjects)
                {
                    _context.Topics.RemoveRange(sub.Topics);
                }
                _context.Subjects.RemoveRange(@class.Subjects);
                _context.Students.RemoveRange(@class.Students);
                _context.Classes.Remove(@class);
                _context.SaveChanges();
                return Json(new { success = true, message = "Class deleted successfully" });
            }
            return Json(new { success = false, message = "Class not found" });
        }
        [HttpPost]
        public IActionResult DeleteSubject(int subjectId)
        {
            var subject = _context.Subjects
                .Include(x=>x.Topics)
                .FirstOrDefault(x=>x.SubjectId == subjectId);
            if (subject != null)
            {
                _context.Topics.RemoveRange(subject.Topics);
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
                return Json(new { success = true, message = "Subject deleted successfully" });
            }
            return Json(new { success = false, message = "Subject not found" });
        }
        [HttpPost]
        public IActionResult DeleteTopic(int topicId)
        {
            var topic = _context.Topics.Find(topicId);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                _context.SaveChanges();
                return Json(new { success = true, message = "Topic deleted successfully" });
            }
            return Json(new { success = false, message = "Topic not found" });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the admin by email
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Email == model.Email && a.PasswordHash == model.Password);

                // If admin is found, verify the password
                if (admin != null)
                {
                   
                        // Create authentication claims
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Email),
                    new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                    new Claim("AdminName", admin.Name.ToString()),
                    // You can add more claims if needed
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            // Optional settings, e.g. whether to persist the cookie across sessions
                            //IsPersistent = model.RememberMe
                        };

                        // Sign in the admin
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                      new ClaimsPrincipal(claimsIdentity), authProperties);

                        // Redirect to the admin dashboard after successful login
                        return RedirectToAction("Dashboard", "Admin");
                    }

                // If admin is not found or password is incorrect
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
