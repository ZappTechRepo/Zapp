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
    
    public partial class Wishlist
    {
        public long Id { get; set; }
        public System.Guid ProductGuid { get; set; }
        public System.Guid AccountGuid { get; set; }
        public System.Guid CustomerGuid { get; set; }
        public int Quantity { get; set; }
    }
}
