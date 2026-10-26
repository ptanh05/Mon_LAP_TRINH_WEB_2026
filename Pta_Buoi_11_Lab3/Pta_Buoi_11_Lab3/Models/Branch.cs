using System.ComponentModel.DataAnnotations;

namespace Pta_Buoi_11_Lab3.Models
{
    public enum Branch
    {
        [Display(Name = "IT - Công nghệ thông tin")]
        IT = 1,

        [Display(Name = "BE - Kinh tế")]
        BE = 2,

        [Display(Name = "CE - Công trình")]
        CE = 3,

        [Display(Name = "EE - Điện - Điện tử")]
        EE = 4
    }
}
