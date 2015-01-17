using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalSolutions.Models
{
    [Table("registrations")]
    public class registration
    {     
        
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            public int? client_id { get; set; }
            public virtual client client { get; set; }
            public int student_id { get; set; }
            public virtual student student { get; set; }
            public int seminar_id { get; set; }
            public virtual seminar seminar { get; set; }
            public bool? Paid { get; set; }
            public bool? Attendend { get; set; }
            public DateTime? Attenddate { get; set; }
            public DateTime Registerdate { get; set; }
            

    }
}
