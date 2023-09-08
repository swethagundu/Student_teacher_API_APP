using System.ComponentModel.DataAnnotations;

namespace STTEWebApp.Models
{
    public class Student
    {

        [Required]
        [Key]
        public int S_ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Marks { get; set; }
    }
}
