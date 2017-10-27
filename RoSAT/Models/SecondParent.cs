using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoSAT.Models
{
    public class SecondParent
    {
        [Required(ErrorMessage = "Required")]
        [DisplayName("Father's Name")]
        public string FathersName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Mother's Name")]
        public string MothersName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Father's Qualification")]
        public string FatherQualification { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Mother's Qualification")]
        public string MotherQualification { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DisplayName("Father's Phone Number")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("[0-9]{10,10}?$", ErrorMessage = "Phone Number Should be 10 Digits")]
        public Decimal FatherPhone { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DisplayName("Mother's Phone Number")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("[0-9]{10,10}?$", ErrorMessage = "Phone Number Should be 10 Digits")]
        public Decimal MotherPhone { get; set; }


        [DisplayName("Father's Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid E-Mail Format")]
        public string FatherEmail { get; set; }

        [DisplayName("Mother's Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid E-Mail Format")]
        public string MotherEmail { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Mother's Occupation")]
        public string MotherOccupation { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Father's Occupation")]
        public string FatherOccupation { get; set; }

        [Required]
        [DisplayName("Father's Salary")]
        public int FatherSalary { get; set; }

        [Required]
        [DisplayName("Mother's Salary")]
        public int MotherSalary { get; set; }



    }
}