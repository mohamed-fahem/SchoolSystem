using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var student = _context.Students.Include(s=>s.Department).ToList();
            return View(student);
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsController/Create
        // GET: Student/Create
        public IActionResult Create()
        {
            var model = new StudentSubjectViewModel
            {
                Departments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }),
                Subjects = _context.Subjects.Select(s => new SelectListItem
                {
                    Value = s.SubjectId.ToString(),
                    Text = s.SubjectName
                })
            };

            return View(model);
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Departments = _context.Departments.Select(s => new SelectListItem
                {
                    Value = s.DepartmentId.ToString(),
                    Text = s.DepartmentName
                });
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    DepartmentId = model.DepartmentId,
                };

                if (model.SelectedSubjects != null)
                {
                    student.Subjects = model.SelectedSubjects
                        .Select(subjectId => new StudentSubject { StudentId = student.StudentId, SubjectId = subjectId })
                        .ToList();
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Subjects = _context.Subjects.Select(s => new SelectListItem
            {
                Value = s.SubjectId.ToString(),
                Text = s.SubjectName
            });

            return View(model);
        }


        // GET: StudentsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            var model= new StudentSubjectViewModel
            {
                Id = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Email = student.Email,
                IsActive = student.IsActive,
                DepartmentId = student.DepartmentId,
                SelectedSubjects = student.Subjects.Select(s => s.SubjectId).ToList(),
                Departments = _context.Departments.Select(s => new SelectListItem
                {
                    Value = s.DepartmentId.ToString(),
                    Text = s.DepartmentName
                }),
                Subjects = _context.Subjects.Select(s => new SelectListItem
                {
                    Value = s.SubjectId.ToString(),
                    Text = s.SubjectName
                })
            };

            return View( model);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentSubjectViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var student = await _context.Students
                    .Include(s => s.Subjects)
                    .FirstOrDefaultAsync(s => s.StudentId == id);

                if (student == null)
                {
                    return NotFound();
                }

                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.Phone = viewModel.Phone;
                student.Email = viewModel.Email;
                student.IsActive = viewModel.IsActive;
                student.Department = _context.Departments.SingleOrDefault(s => s.DepartmentId == viewModel.DepartmentId);

                // Remove existing subjects
                student.Subjects.Clear();

                if (viewModel.SelectedSubjects != null)
                {
                    student.Subjects = viewModel.SelectedSubjects
                        .Select(subjectId => new StudentSubject { StudentId = student.StudentId, SubjectId = subjectId })
                        .ToList();

                }

                _context.Update(student);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            viewModel.Subjects = _context.Subjects.Select(s => new SelectListItem
            {
                Value = s.SubjectId.ToString(),
                Text = s.SubjectName
            });

            return View(viewModel);
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
