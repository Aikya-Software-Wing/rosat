namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        [Required]
        public int? Category { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Level")]
        public int? ELevel { get; set; }

        [Column(TypeName = "numeric")]
        [Required]
        [Range(0,999,ErrorMessage="Enter valid position")]
        public int Position { get; set; }

        [StringLength(255)]
        [Display(Name = "Certificate ID")]
        public string CertificateId { get; set; }

        [StringLength(255)]
        [Display(Name = "Certificate Issuing Authority")]
        public string CertificateIssuingAuthority { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Certificate Earned Date")]
        [DataType(DataType.Date)]
        public DateTime? CertificateEarnedDate { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Certificate Photo")]
        public byte[] CertificatePhoto { get; set; }

        public virtual EventLevel EventLevel { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual Student Student { get; set; }
    }
}
