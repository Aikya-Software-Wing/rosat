namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentTakesQuiz")]
    public partial class StudentTakesQuiz
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        public Guid? QuizId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Score { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Attempts { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual Student Student { get; set; }
    }
}
