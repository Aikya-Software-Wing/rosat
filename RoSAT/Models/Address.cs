namespace RoSAT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        public int? AType { get; set; }

        [StringLength(250)]
        public string Addr { get; set; }

        public virtual AddressType AddressType { get; set; }

        public virtual Student Student { get; set; }
    }
}
