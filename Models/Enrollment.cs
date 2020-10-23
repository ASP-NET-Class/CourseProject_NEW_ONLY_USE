using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public enum LetterGrade
    {
        New, Poor, Developing, Excellent
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int SwimmerId { get; set; }
        public int SessionId { get; set; }
        public Swimmer Swimmer { get; set; }
        public Session Session { get; set; }
        public Coach Coach { get; set; }
        [DisplayFormat(NullDisplayText = "No Grade")]
        public LetterGrade? LetterGrade { get; set; }

        /*internal void SetLetterGrade(object p)
        {
            throw new NotImplementedException();
        }

        internal object GetLetterGrade()
        {
            throw new NotImplementedException();
        }*/
    }
}