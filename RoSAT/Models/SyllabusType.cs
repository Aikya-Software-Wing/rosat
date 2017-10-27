namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SyllabusType")]
    public partial class SyllabusType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SyllabusType()
        {
            Marks = new HashSet<Mark>();
            SubjectTypes = new HashSet<SubjectType>();
        }

        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        [Required]
        public decimal Year { get; set; }

        public bool IsCGPA { get; set; }

        [NotMapped]
        [Required]
        public int Semester { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectType> SubjectTypes { get; set; }
    }
}
