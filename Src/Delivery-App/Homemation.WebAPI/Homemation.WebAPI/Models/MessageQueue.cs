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
    
    public partial class MessageQueue
    {
        public System.Guid Rowguid { get; set; }
        public System.DateTime Created { get; set; }
        public int Retries { get; set; }
        public byte[] Mailer { get; set; }
        public bool IsText { get; set; }
        public string Message { get; set; }
    }
}