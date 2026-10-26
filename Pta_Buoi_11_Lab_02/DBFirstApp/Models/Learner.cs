using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DBFirstApp.Models;

public partial class Learner
{
    public int LearnerId { get; set; }

    public string? LastName { get; set; }

    public string? FirstMidName { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int MajorId { get; set; }

    [ValidateNever]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ValidateNever]
    public virtual Major? Major { get; set; }
}
