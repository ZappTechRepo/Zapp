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
    
    public partial class ProductBranding
    {
        public long Id { get; set; }
        public System.DateTime Created { get; set; }
        public System.Guid Guid { get; set; }
        public byte[] Checksum { get; set; }
        public long ProductId { get; set; }
        public string BrandType { get; set; }
        public string BrandSubType { get; set; }
        public string Colours { get; set; }
        public string ProductColour { get; set; }
        public string ProductSize { get; set; }
        public string ExpandedProductCode { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
