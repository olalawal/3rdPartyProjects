using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalSolutions.Models
{
      [Table("seminars")]
    public class seminar
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string TitleHtml { get; set; }
        public string EventDetailsHtml { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }      
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactWebsite { get; set; }
        public virtual ICollection<registration> registrations { get; set; }
        
    }
}
