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
    [Table("messages")]
    [DataContract]
    public class message
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     [DataMember]public int id { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public string Body { get; set; }
        [DataMember]
        [Required]
        public string Subject { get; set; }
        [DataMember]
        [Required]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        

    }
}
