namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Addresses = new HashSet<Address>();
            AreaInterests = new HashSet<AreaInterest>();
            Events = new HashSet<Event>();
            JobProjects = new HashSet<JobProject>();
            Marks = new HashSet<Mark>();
            Parents = new HashSet<Parent>();
            Schools = new HashSet<School>();
            Skills = new HashSet<Skill>();
          
        }

        public Guid Id { get; set; }

        [StringLength(10)]
        [DisplayName("USN")]
        [RegularExpression("^[0-9][R][N][0-9]{2,2}[A-Z][A-Z]\\d{3}$", ErrorMessage = "Invalid USN (Enter in Capitals)")]
        public string Usn { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Aadhar Number")]
        [AtLeastOneRequired("Usn,AadharNo", ErrorMessage = "At Least one required of USN or Aadhar Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [RegularExpression("[0-9]{12,12}?$", ErrorMessage = "Aadhar Number Should be 12 Digits")]
        public decimal? AadharNo { get; set; }

        [StringLength(225)]
        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Full Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        [DisplayName("Gender")]
        public int? Gender { get; set; }

        [Column(TypeName = "date")]
        [MinimumAge(17, ErrorMessage = "You Should be Atleast 17")]
        [DisplayName("Date of Birth")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Please Select Admission Quota")]
        [DisplayName("Admission Quota")]
        public int? AdmissionQuota { get; set; }

        [Required(ErrorMessage = "Please Select Quota")]
        [DisplayName("Major Quota")]
        public int? MajorQuota { get; set; }

        [Required(ErrorMessage = "Please Select Quota")]
        [DisplayName("Minor Quota")]
        public int? MinorQuota { get; set; }


        [Required(ErrorMessage = "Please Select Department")]
        [DisplayName("Department")]
        public int? Department { get; set; }

        [StringLength(255)]
        public string EmailId { get; set; }

        [Column(TypeName = "numeric")]
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DisplayName("Phone Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("[0-9]{10,10}?$", ErrorMessage = "Phone Number Should be 10 Digits")]
        public decimal? PhoneNumber { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Photo")]
        public byte[] photo { get; set; }

        [Required]
        [DisplayName("Year of Joining")]
        [Range(2001, 2016, ErrorMessage="Enter valid year of joining")]
        public int? YearOfJoining { get; set; }

        [Required]
        [DisplayName("Semster")]
        [Range(1, 8)]
        public int Semester { get; set; }

        public virtual MajorQuota MajorQuotas { get; set; }
        public virtual MinorQuota MinorQuotas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreaInterest> AreaInterests { get; set; }

        public virtual Department Department1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        public virtual Gender Gender1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobProject> JobProjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents { get; set; }

        public virtual Quota Quota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School> Schools { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Skill> Skills { get; set; }

        
    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }

            return false;
        }
    }
    public class AtLeastOneRequired : ValidationAttribute
    {
        public string OtherPropertyNames;

        /// <param name="otherPropertyNames">Multiple property name with comma(,) separator</param>
        public AtLeastOneRequired(string otherPropertyNames)
        {
            OtherPropertyNames = otherPropertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] propertyNames = OtherPropertyNames.Split(',');
            bool isAllNull = true;
            foreach (var i in propertyNames)
            {
                var p = validationContext.ObjectType.GetProperty(i);
                var val = p.GetValue(validationContext.ObjectInstance, null);
                if (val != null && val.ToString().Trim() != "")
                {
                    isAllNull = false;
                    break;
                }
            }

            if (isAllNull)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return null;
            }
        }

    }
}
