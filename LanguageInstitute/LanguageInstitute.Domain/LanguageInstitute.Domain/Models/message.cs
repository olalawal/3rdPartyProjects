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
    [Table("messages")]
    [DataContract]
    public class message
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     [DataMember]public int id { get; set; }
       
        [DataMember]
        [Required]
        [Display(Name = "Your name")]
        public string Name { get; set; }
        [DataMember]       
        [Required]
        [Display(Name = "Your Message")]
        public string Body { get; set; }
        [DataMember]
        [Required]
        public string Subject { get; set; }
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
        public string Phone { get; set; }
            [DataMember]
        public DateTime? MessageDate { get; set; } 
        

    }


}
