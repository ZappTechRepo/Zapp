//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homemation.WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierBranding
    {
        public SupplierBranding()
        {
            this.SupplierBrandPricings = new HashSet<SupplierBrandPricing>();
        }
    
        public long Id { get; set; }
        public System.DateTime Created { get; set; }
        public System.Guid Guid { get; set; }
        public byte[] Checksum { get; set; }
        public string PrintType { get; set; }
        public string PrintSubType { get; set; }
        public string Colours { get; set; }
        public long SupplierId { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierBrandPricing> SupplierBrandPricings { get; set; }
    }
}
