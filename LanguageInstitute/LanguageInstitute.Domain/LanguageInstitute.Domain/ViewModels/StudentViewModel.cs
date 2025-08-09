using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageInstitute.Models
{
    public class StudentViewModel
    {

        public int student_id { get; set; }


        [Required(ErrorMessage = "The first name field is required")]
        [Display(Name = "Your first name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "The last name field is required")]
        [Display(Name = "Your last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "The address 1 field is required")]
        [Display(Name = "Your address 1")]
        public string Address1 { get; set; }

        
        [Display(Name = "Your address 2")]
        public string Address2 { get; set; }


         [Required(ErrorMessage = "The city field is required")]
        [Display(Name = "Your city")]
        public string City { get; set; }


         [Required(ErrorMessage = "The state/province field is required")]
        [Display(Name = "Your state/province")]
        public string StateProvince { get; set; }

        
        [Display(Name = "Your ZIP/Postal code")]
        public string ZipPostalCode { get; set; }


         [Required(ErrorMessage = "The country field is required")]
        [Display(Name = "Your country")]
        public string Country { get; set; }

        
        [Display(Name = "Your  description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "The email address field is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        [Display(Name = "Your Email address")]
        public string Email { get; set; }


         [Required(ErrorMessage = "The phone number field is required")]
        [Display(Name = "Your phone number")]
        public string Phone { get; set; }

        
        [Display(Name = "Your fax number")]
        public string Fax { get; set; }

        

        [Display(Name = "Your Bank Account #")]
        public string BankAccountNumber { get; set; }

        
        public bool IsActive { get; set; }

        public List<Countries>  Countries { get; set; }   


        public List<seminar> Seminars { get; set; }   
    }
}