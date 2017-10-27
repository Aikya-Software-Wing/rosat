using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace RoSAT.Models
{
    public class SecondAddress
    {
        [Required(ErrorMessage ="Enter Address")]
        [DisplayName("Permanent Address :")]
        [DataType(DataType.MultilineText)]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [DisplayName("Present Address :")]
        [DataType(DataType.MultilineText)]
        public string PresentAddress { get; set; }
    }
}