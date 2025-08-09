using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageInstitute.Models
{
    public class SeminarEntity
    {

        public int seminar_id { get; set; }

        
        public int? instructor_id { get; set; }
        
        [Required]
        [Display(Name = "Title Html")]
        public string TitleHtml { get; set; }
        
        [Required]
        [Display(Name = "Event Details Html")]
        public string EventDetailsHtml { get; set; }
        
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Required]
        [Display(Name = "End Date")]
        public DateTime Enddate { get; set; }
        
        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Required]
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        
        [Display(Name = "Zip/Postal Code")]
        public string ZipPostalCode { get; set; }
        
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        
        [Display(Name = "Fax Number")]
        public string Fax { get; set; }
        
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string ContactEmail { get; set; }
        
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
        
        [Display(Name = "Contact Website")]
        public string ContactWebsite { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
      
        public string Starttime { get; set; }
        
        public string Endtime { get; set; }
        
        public string EarlyBirdPrice { get; set; }
        
        public string NormalPrice { get; set; }


    }
}
