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

    [Table("Countries")]
    [DataContract]
    public class Countries 
    { 
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int id { get; set; } 
        [DataMember]
        public string Code { get; set; }   
    
        [DataMember]
        public string Country { get; set; }   
    }
}
