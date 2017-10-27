namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubSkill
    {
        public Guid Id { get; set; }

        public Guid? SkillId { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Skill Name")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Certificate ID")]
        public string CertificateId { get; set; }

        [StringLength(255)]
        [Display(Name = "Certificate Issuing Authority")]
        public string CertificateIssuingAuthority { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Certificate Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? CertificateExpiryDate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Certificate Earned Date")]
        [DataType(DataType.Date)]
        public DateTime? CertificateEarnedDate { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Certificate Photo")]
        public byte[] CertificatePhoto { get; set; }

        public virtual Skill Skill { get; set; }

        [NotMapped]
        public int Category { get; set; }
    }
}
