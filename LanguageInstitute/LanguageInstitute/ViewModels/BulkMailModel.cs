using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LanguageInstitute.Models
{
    public class BulkMailModel
    {
        public registration Registration { get; set; }

        public List<student> Students { get; set; }

        public List<seminar> Seminars { get; set; }

        public string[] Filteres { get; set; }

        public int seminar_id { get; set; }   
    }
}