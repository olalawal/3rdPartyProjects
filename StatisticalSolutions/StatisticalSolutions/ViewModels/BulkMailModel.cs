using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatisticalSolutions.Models;

namespace StatisticalSolutions.ViewModels
{
    public class BulkMailModel
    {
        public registration Registration { get; set; }

        public List<student> Students { get; set; } 
    }
}