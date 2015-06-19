using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StatisticalSolutions.Models 
{
    public class WorkshopViewModel 
    {
        public int seminar_id { get; set; }

        public int? instructor_id { get; set; }

        [Required(ErrorMessage = "The title html field is required")]
        [Display(Name = "Title Html")]
        public string TitleHtml { get; set; }

        [Required(ErrorMessage = "The event details html field is required")]
        [Display(Name = "Event Details Html")]
        public string EventDetailsHtml { get; set; }

        [Required(ErrorMessage = "The description field is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The start date field is required")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "The end date field is required")]
        [Display(Name = "End Date")]
        public DateTime? Enddate { get; set; }

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


        [Required(ErrorMessage = "The email address field is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Thr Email address is not valid")]
        [Display(Name = "Your Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The phone number field is required")]
        [Display(Name = "Your phone number")]
        public string Phone { get; set; }

        [Display(Name = "Your fax number")]
        public string Fax { get; set; } 
        
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "The contact email address is not valid")]
        public string ContactEmail { get; set; }
        
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
        
        [Display(Name = "Contact Website")]
        public string ContactWebsite { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "The start time field is required")]
        [Display(Name = "Start Time")]
        public string Starttime { get; set; }

        [Required(ErrorMessage = "The end time field is required")]
        [Display(Name = "End Time")]
        public string Endtime { get; set; }
        
        public string EarlyBirdPrice { get; set; }
        
        public string NormalPrice { get; set; }

        public List<Countries> Countries { get; set; }

        public List<instructor> Instructors { get; set; }

    }
}