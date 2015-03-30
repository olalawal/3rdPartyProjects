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
    [Table("clients")]
    [DataContract]
    public class client
    {     
        
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            [DataMember]
            public int client_id { get; set; }   
            [Required]
            [DataMember]
            public string Name { get; set; }
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
            public string Phone { get; set; }
            [DataMember]
            public string Fax { get; set; }        

    }
}
