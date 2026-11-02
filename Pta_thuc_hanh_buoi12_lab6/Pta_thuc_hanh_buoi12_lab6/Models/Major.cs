namespace Pta_thuc_hanh_buoi12_lab6.Models
{
    public class Major
    {
        public Major()
        {
            Learners = new HashSet<Learner>();
        }
        public int MajorID { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public virtual ICollection<Learner> Learners { get; set; }
    }
}

