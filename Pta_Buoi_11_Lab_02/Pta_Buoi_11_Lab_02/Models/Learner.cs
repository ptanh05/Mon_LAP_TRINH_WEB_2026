using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pta_Buoi_11_Lab_02.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public int LearnerID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstMidName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }
        [ValidateNever]
        public virtual Major? Major { get; set; }
        [ValidateNever]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
