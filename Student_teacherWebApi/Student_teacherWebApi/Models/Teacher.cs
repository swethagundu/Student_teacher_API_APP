using System.ComponentModel.DataAnnotations;

namespace Student_teacherWebApi.Models
{
    public class Teacher
    {
        [Required]
        [Key]
        public int T_ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
