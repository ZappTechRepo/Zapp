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
    
    public partial class EmailSetting
    {
        public long id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public System.Guid guid { get; set; }
        public byte[] checksum { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public bool Authenticate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReplyAddress { get; set; }
        public bool UseMessageQueueing { get; set; }
    }
}
