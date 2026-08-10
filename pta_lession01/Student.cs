using System;

namespace pta_lession01
{
    internal sealed class Student
    {
        public Student(
            string id,
            string fullName,
            DateOnly dateOfBirth,
            string gender,
            string email,
            string phone,
            string major,
            double gpa,
            string status)
        {
            Id = id.Trim();
            FullName = fullName.Trim();
            DateOfBirth = dateOfBirth;
            Gender = gender.Trim();
            Email = email.Trim();
            Phone = phone.Trim();
            Major = major.Trim();
            Gpa = gpa;
            Status = status.Trim();
        }

        public string Id { get; private set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Major { get; set; }
        public double Gpa { get; set; }
        public string Status { get; set; }

        public void SetId(string id)
        {
            Id = id.Trim();
        }
    }
}
