using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.ViewModels
{
    public class StudentSubjectViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<int> SelectedSubjects { get; set; } = default!;
        public IEnumerable<SelectListItem> Subjects { get; set; } = Enumerable.Empty<SelectListItem>();
        
    }
}
