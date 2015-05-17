using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatisticalSolutions.Models;

namespace StatisticalSolutions.ViewModels
{
    public class SeminarInstructorModel
    {
        public seminar seminar { get; set; }

        public instructor Instructor { get; set; } 
    }
}