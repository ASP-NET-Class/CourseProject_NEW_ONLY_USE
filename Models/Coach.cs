using System.Collections.Generic;

namespace CourseProject.Models
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string CoachName { get; set; }
        public string CoachPhone { get; set; }
        public string CoachEmail { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
