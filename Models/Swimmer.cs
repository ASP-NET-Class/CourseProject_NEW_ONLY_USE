using System.Collections.Generic;

namespace CourseProject.Models
{
    public class Swimmer
    {
        public int SwimmerId { get; set; }
        public string SwimmerName { get; set; }
        public string SwimmerPhone { get; set; }
        public string SwimmerGender { get; set; }
        public string SwimmerDob { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
