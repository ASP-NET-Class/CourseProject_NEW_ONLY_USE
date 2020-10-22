using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string SessionTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int SeatCapacity { get; set; }
        [DataType(DataType.Time)]
        public DateTime DailyStartTime { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
