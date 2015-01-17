using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalSolutions.Models
{
    [Table("clients")]
    public class client
    {     
        
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            [Required]
            public string Name { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string StateProvince { get; set; }
            public string ZipPostalCode { get; set; }
            public string Country { get; set; }
            public string Description { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }        

    }
}
