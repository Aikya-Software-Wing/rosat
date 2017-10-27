namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("MinorQuota")]
    public partial class MinorQuota
    {
        public MinorQuota()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }


        public virtual ICollection<Student> Students { get; set; }


    }
}
