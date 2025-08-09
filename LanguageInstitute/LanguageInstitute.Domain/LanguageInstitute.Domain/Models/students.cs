using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstitute.Models
{
      [Table("students")]
    [DataContract]
    public class student
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
          [DataMember]
          public int student_id { get; set; }  

        [DataMember]
          [Required]
          [Display(Name = "Your first name")]
        public string FirstName { get; set; } 

        [DataMember]
        [Required]
        [Display(Name = "Your last name")]
        public string LastName { get; set; }

        [DataMember]
        
        [Display(Name = "Your address 1")]
        public string Address1 { get; set; }

        [DataMember]
        [Display(Name = "Your address 2")]
        public string Address2 { get; set; }

        [DataMember]
        
        [Display(Name = "Your city")]
        public string City { get; set; }

        [DataMember]
      
        [Display(Name = "Your state/province")]
        public string StateProvince { get; set; }

        [DataMember]
        [Display(Name = "Your ZIP/Postal code")]
        public string ZipPostalCode { get; set; }

        [DataMember]
     
        [Display(Name = "Your country")]
        public string Country { get; set; }

        [DataMember]
        [Display(Name = "Your  description")]
        public string Description { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        [Display(Name = "Your Email address")]
        public string Email { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Your phone number")]
        public string Phone { get; set; }

        [DataMember]
        [Display(Name = "Your fax number")]
        public string Fax { get; set; }

        [DataMember]
        
        [Display(Name = "Your Bank Account #")]
        public string BankAccountNumber { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
       // public virtual ICollection<registration> registrations  { get; set; }
    }
}
