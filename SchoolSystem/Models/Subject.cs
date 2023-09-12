namespace SchoolSystem.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        

        public virtual ICollection<StudentSubject> Students { get; set; }
    }
}
