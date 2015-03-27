using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalSolutions.Models
{
      [Table("seminars")]
      [DataContract]
    public class seminar
    {
        [Key]
          [DataMember]
          [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int seminar_id { get; set; }   
        [DataMember]
        public string TitleHtml { get; set; }
        [DataMember]
        public string EventDetailsHtml { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime Enddate { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string StateProvince { get; set; }
        [DataMember]
        public string ZipPostalCode { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string ContactEmail { get; set; }
        [DataMember]
        public string ContactPhone { get; set; }
        [DataMember]     
        public string ContactWebsite { get; set; }
        [DataMember]
        public virtual ICollection<registration> registrations { get; set; }
        
    }


}
