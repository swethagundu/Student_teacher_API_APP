using System.ComponentModel.DataAnnotations;
namespace STTEWebApp.Models
{
    public class Teacher
    {
        [Required]
        [Key]
        public int T_ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
