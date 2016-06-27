using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.Models
{
    public class Dashboard
    {
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<Customer> Customer { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public int TotalDocuments { get { return Documents.Count(); } }
        public int TotalCustomers { get { return Customer.Count(); } }
        public int TotalProducts{ get { return Product.Count(); } }
    }
}