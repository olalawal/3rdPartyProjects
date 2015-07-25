using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LanguageInstitute.Models 
{
    public class RegistrationViewModel
    {
        public int registration_id { get; set; }

        [Required(ErrorMessage = "The workshop field is required")]
        public int seminar_id { get; set; }

        public seminar Seminar { get; set; } 
           
        public int? client_id { get; set; }

        [Display(Name = "Your company")]
        public string ClientName { get; set; }

        public int student_id { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [Display(Name = "Your first name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "The last name is required")]
        [Display(Name = "Your last name")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "The email address number is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "The email address is not valid")]
        [Display(Name = "Your Email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The phone number is required")]
        [Display(Name = "Your phone number")]
        public string Phone { get; set; }

                 
        [Required(ErrorMessage = "The Start Date field is required")]
        public string Starttime { get; set; }
      
     
        public List<seminar> Seminars { get; set; }

      
        public List<Countries> Countries { get; set; }

   
        public List<string> Companies { get; set; }  
    }
}