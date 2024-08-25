using System.Collections.Generic;

namespace UelApplication.Models;

public class StudentInfo
{
        public string Name { get; set; }
        public string Program { get; set; }
        public string ASU_ID { get; set; }
        public string UEL_ID { get; set; }
        public string Semester { get; set; }
        public string AcademicYear { get; set; }
        public string SubmissionDate { get; set; }
        public List<Course> Courses { get; set; }
}