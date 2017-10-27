using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoSAT.Models
{
    [MetadataType(typeof(MarkMetaData))]

  
   
        public class MarkMetaData
    {
            [Required]
            public int SubType { get; set; }
        
            int SylType { get; set; }

            [Required]
            public decimal Sem { get; set; }

            [Required]
            [Range(0,25,ErrorMessage ="Range Exceeded")]
            public decimal InternalMarks { get; set; }

            [Required]
            [Range(0,100,ErrorMessage ="Range Exceeded")]
            public decimal ExternalMarks { get; set; }

            public bool Ispass { get; set; }
        }
}