using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Context;
using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentsController
        public ActionResult Index()
        {
            var student = _context.Students.ToList();
            return View(student);
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            var item = _context.Subjects.ToList();
            var model = new StudentSubjectViewModel();
            model.AvailableSubjects = item.Select(av => new CheckBoxItems()
            {
                Id = av.SubjectId,
                Title = av.SubjectName,
                IsChecked= false
            }).ToList();
            return View(model);
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {

                var studentSubject = new List<StudentSubject>();

                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    
                };
                _context.Add(student);
                _context.SaveChanges();

                foreach (var subject in model.AvailableSubjects)
                {
                    if (subject.IsChecked==true)
                    {
                        studentSubject.Add(new StudentSubject
                        {
                            StudentId = model.Id,
                            SubjectId = subject.Id
                        });
                    }
                }
                foreach (var item in studentSubject)
                {
                    _context.StudentSubjects.Add(item);
                }

                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.AvailableSubjects = GetAvailableSubjects();
            return View(model);
        }


        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private List<CheckBoxItems> GetAvailableSubjects()
        {
            return _context.Subjects
                .Select(s => new CheckBoxItems
                {
                    Id = s.SubjectId,
                    Title = s.SubjectName,
                    IsChecked = false
                })
                .ToList();
        }
    }
}
