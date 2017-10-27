namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubjectType")]
    public partial class SubjectType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubjectType()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Semester { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        public bool? IsLab { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MinMarks { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MinExternalMarks { get; set; }

        public int SubId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MaxCredits { get; set; }

        public int? Dept { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }

        public virtual SyllabusType SyllabusType { get; set; }
    }
}
