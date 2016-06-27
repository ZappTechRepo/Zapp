using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.Models
{
    public class JsonScriptResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public string data { get; set; }
    }
}