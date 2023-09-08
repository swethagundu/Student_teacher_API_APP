using System.ComponentModel.DataAnnotations;
namespace STTEWebApp.Models
{
    public class Course
    {
        [Required]
        [Key]
        public int C_Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
