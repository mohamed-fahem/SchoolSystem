using SchoolSystem.Models;

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
        //public int DepartmentId { get; set; }
        //public Department Department { get; set; }

        public List<CheckBoxItems> AvailableSubjects { get; set; }
    }
}
