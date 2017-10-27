namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quiz")]
    public partial class Quiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quiz()
        {
            Questions = new HashSet<Question>();
            StudentTakesQuizs = new HashSet<StudentTakesQuiz>();
        }

        public Guid Id { get; set; }

        public int? Department { get; set; }

        public Guid? TeacherId { get; set; }

        [StringLength(1000)]
        public string QuizName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NoOfQues { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Semester { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Batch { get; set; }

        [StringLength(1)]
        public string Section { get; set; }

        public virtual Department Department1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTakesQuiz> StudentTakesQuizs { get; set; }
    }
}
