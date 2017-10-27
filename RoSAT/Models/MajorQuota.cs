using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoSAT.Models
{
    //[Table("MajorQuota")]
    public class MajorQuota
    {
        public MajorQuota()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }


        public virtual ICollection<Student> Students { get; set; }
    }
}