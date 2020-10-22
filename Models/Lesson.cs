using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; }
        public string SkillLevel { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tuition { get; set; }
    }
}
