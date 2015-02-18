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
    [Table("registrations")]
    [DataContract]
    public class registration
    {     
        
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int id { get; set; }
            [DataMember]
            public int? client_id { get; set; }
            [DataMember]
            public virtual client client { get; set; }
            [DataMember]
            public int student_id { get; set; }
            [DataMember]
            public virtual student student { get; set; }
            [DataMember]
            public int seminar_id { get; set; }
            [DataMember]
            public virtual seminar seminar { get; set; }
            [DataMember]
            public bool? Paid { get; set; }
            [DataMember]
            public bool? Attendend { get; set; }
            [DataMember]
            public DateTime? Attenddate { get; set; }
            [DataMember]
            public DateTime Registerdate { get; set; }
            

    }
}
