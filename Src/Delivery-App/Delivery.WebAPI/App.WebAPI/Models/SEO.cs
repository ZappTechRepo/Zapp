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
    
    public partial class SEO
    {
        public long id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public System.Guid guid { get; set; }
        public byte[] checksum { get; set; }
        public string PageTitle { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Url { get; set; }
        public Nullable<long> CategoryId { get; set; }
        public Nullable<long> ProductId { get; set; }
    }
}