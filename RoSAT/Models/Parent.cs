namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parent")]
    public partial class Parent
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        public int? PType { get; set; }

        [StringLength(225)]
        public string Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PhoneNo { get; set; }

        [StringLength(255)]
        public string EmailId { get; set; }

        [StringLength(255)]
        public string Qualification { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        public int? SalaryType { get; set; }

        public virtual ParentType ParentType { get; set; }

        public virtual SalaryType SalaryType1 { get; set; }

        public virtual Student Student { get; set; }
    }
}
