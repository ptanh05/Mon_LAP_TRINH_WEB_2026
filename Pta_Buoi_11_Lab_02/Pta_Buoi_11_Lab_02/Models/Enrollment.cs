using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pta_Buoi_11_Lab_02.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int LearnerID { get; set; }
        public float Grade { get; set; }
        [ValidateNever]
        public virtual Learner? Learner { get; set; }
        [ValidateNever]
        public virtual Course? Course { get; set; }
    }
}
