namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JobProject
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int? Category { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Company or Project Name")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Descri { get; set; }

        [Required]
        [Display(Name = "Duration (weeks)")]
        [Range(0, int.MaxValue)]
        public int Duration { get; set; }

        [Column(TypeName = "numeric")]
        [Range(0, 99999999)]
        public decimal? Salary { get; set; }

        public virtual JobProjectsCategory JobProjectsCategory { get; set; }

        public virtual Student Student { get; set; }
    }
}
