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
        public int? instructor_id { get; set; } 
        [DataMember]
        [Required]
        [Display(Name = "Title Html")]
        public string TitleHtml { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Event Details Html")]
        public string EventDetailsHtml { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "End Date")]
        public DateTime Enddate { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [DataMember]      
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [DataMember]        
        [Display(Name = "City")]
        public string City { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        [DataMember]       
        [Display(Name = "Zip/Postal Code")]
        public string ZipPostalCode { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [DataMember]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [DataMember]
        [Display(Name = "Fax Number")]
        public string Fax { get; set; }
        [DataMember]
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string ContactEmail { get; set; }
        [DataMember]
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
        [DataMember]
        [Display(Name = "Contact Website")]
        public string ContactWebsite { get; set; }
        [DataMember]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [DataMember]
        public virtual ICollection<registration> registrations { get; set; }
        [DataMember]
        public virtual instructor instructor { get; set; }
            [DataMember]
        public string Starttime { get; set; }
            [DataMember]
        public string Endtime { get; set; }
           [DataMember]
          public string EarlyBirdPrice { get; set; }
           [DataMember]
          public string NormalPrice { get; set; }


        
    }


}
