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
    
    public partial class SerialsCatalog
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string ItemCode { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string RawBarCode { get; set; }
        public int SerializationStatus { get; set; }
    }
}