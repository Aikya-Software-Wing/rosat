namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AreaInterest")]
    public partial class AreaInterest
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        [StringLength(255)]
        public string Area { get; set; }

        public virtual Student Student { get; set; }
    }
}
