using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoSAT.Models
{
    [MetadataType(typeof(ParentMetaData))]
    public partial class Parent { }
    public class ParentMetaData
    {
        [DisplayName("USN")]
        public Guid StudentId { get; set; }


        [DisplayName("(Parent/Guardian)")]
        [Required(ErrorMessage = "Please Select Parent")]
        public int PType { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Enter Name")]

        public string Name { get; set; }

        [DisplayName("Phone +91")]
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("[0-9]{10,10}?$", ErrorMessage = "Phone Number Should be 10 Digits")]
        public Decimal PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid E-Mail Format")]
        [DisplayName("Email ID")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Select Qualification")]

        [DisplayName("Qualification")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Please Select Occupation")]

        [DisplayName("Occupation")]
        public String Occupation { get; set; }

        [Required(ErrorMessage = "Please Enter Salary")]
        [DisplayName("Annual Salary (in LPA)")]
        public Decimal AnnualSalary { get; set; }


    }
 
}