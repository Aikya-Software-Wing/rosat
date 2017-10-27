namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mark
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        [Required]
        public int SubType { get; set; }

        [Required]
        public int SylType { get; set; }

        [Required]
        [Range(0, 100,ErrorMessage ="Enter valid semester")]
        public int Sem { get; set; }

        [Required]
        [Range(0, 100)]
        public int? InternalMarks { get; set; }

        [Required]
        [Range(0, 100)]
        public int? ExternalMarks { get; set; }

        public bool? IsPass { get; set; }

        public virtual Student Student { get; set; }

        public virtual SubjectType SubjectType { get; set; }

        public virtual SyllabusType SyllabusType { get; set; }
    }
}
