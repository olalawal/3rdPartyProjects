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
            [Required(ErrorMessage = "The workshop field is required")]
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
            [DataMember]
           // [Required(ErrorMessage = "The Start Date field is required")]
            public DateTime StartDate { get; set; }


             [NotMapped]
             [Required(ErrorMessage = "The Start Date field is required")]
            public string Starttime { get; set; }
            [NotMapped]
            public string Endtime { get; set; }
           [NotMapped]
           public List<DisplayDateTime> StartDates { get; set; }



    }

}
