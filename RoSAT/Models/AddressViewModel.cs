using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoSAT.Models
{
    public class AddressViewModel
    {
        [Required]
        [Display(Name ="Present Address")]
        [DataType(DataType.MultilineText)]
        public string presentAddress { get; set; }

        [Required]
        [Display(Name = "Permanent Address")]
        [DataType(DataType.MultilineText)]
        public string permanentAddress { get; set; }
    }
}