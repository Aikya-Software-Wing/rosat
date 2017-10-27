namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Choice
    {
        public Guid Id { get; set; }

        public Guid? Question { get; set; }

        [Column("Choice", TypeName = "numeric")]
        public decimal? Choice1 { get; set; }

        public virtual Question Question1 { get; set; }
    }
}
