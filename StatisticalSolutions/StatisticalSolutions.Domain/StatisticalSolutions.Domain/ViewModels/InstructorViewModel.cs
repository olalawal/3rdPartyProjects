using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace StatisticalSolutions.Models
{
    public class InstructorViewModel 
    {
        
        public int instructor_id { get; set; }

        [Required(ErrorMessage = "The instructor name field is required")]        
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

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

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
        public string ImageName { get; set; }
        
        public string ImagePath { get; set; }
        
        public string DetailsHtml { get; set; } 

        public List<Countries> Countries { get; set; }

        //[Required(ErrorMessage = "Image File is required")]
        [Display(Name = "Image File")]
        public HttpPostedFileBase File { get; set; } 

    }
}