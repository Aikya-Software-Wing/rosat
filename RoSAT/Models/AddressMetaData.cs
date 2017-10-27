using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoSAT.Models
{
    [MetadataType(typeof(AddressMetaData))]
    public partial class Address { }
    public class AddressMetaData
    {
        
        //[Required(ErrorMessage="Required USN")]
        [DisplayName("USN ")]
        public int StudentId { get; set; }

        [Required(ErrorMessage ="Please Select Type Of Your Address")]
        [DisplayName("Address Type")]
        public int AType { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [DisplayName("Address ")]
        [DataType(DataType.MultilineText)]
        public string Addr { get; set; }

       // public bool Selected { get; set; }
    }
}