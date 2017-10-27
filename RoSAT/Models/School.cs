namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("School")]
    public partial class School
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Required]
        public int? Board { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Medium Of Instruction")]
        public string MediumInstruction { get; set; }

        [Display(Name="GPA")]
        public bool IsGPA { get; set; }

        [Required]
        [Display(Name = "Total Percentage")]
        public decimal PercentageMarks { get; set; }

        [Required]
        [Display(Name = "Standard")]
        public int? SchoolTypeId { get; set; }

        public bool IsUrban { get; set; }

        public virtual BoardType BoardType { get; set; }

        public virtual SchoolType SchoolType { get; set; }

        public virtual Student Student { get; set; }
    }
}
