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
    
    public partial class CustomerCommunication
    {
        public System.Guid salesrepcommunicationguid { get; set; }
        public System.Guid customer { get; set; }
    
        public virtual SalesRepCommunication SalesRepCommunication { get; set; }
    }
}
