namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Choices = new HashSet<Choice>();
        }

        public Guid Id { get; set; }

        public Guid? QuizId { get; set; }

        [Column("Question")]
        [StringLength(1000)]
        public string Question1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Solution { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Choice> Choices { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
