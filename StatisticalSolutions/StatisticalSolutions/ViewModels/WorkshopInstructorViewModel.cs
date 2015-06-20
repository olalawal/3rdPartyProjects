using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace StatisticalSolutions.Models
{
    public class WorkshopInstructorViewModel
    {

        public seminar Seminar { get; set; }

        public instructor Instructor { get; set; }   

        public List<seminar> Seminars { get; set; } 

        public List<instructor> Instructors { get; set; }

        
    }
}