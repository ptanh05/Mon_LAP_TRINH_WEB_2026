using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pta_thuc_hanh_buoi12_lab6.Models
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        [ValidateNever]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

