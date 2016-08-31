using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.Models
{
    public class OrderDocument
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public double TotalIncl { get; set; }
        public double TotalExcl { get; set; }
        public double Discount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ReferenceNo { get; set; }
        public string InvoiceNo { get; set; }
        public bool Delivered { get; set; }
    }

    public class OrderLine
    {
        public int lineID { get; set; }
        public int lineQTY { get; set; }
        public double linePrice { get; set; }
        public double lineTotal { get; set; }
    }

    public class CompletedDelivery
    {
        public String Signature { get; set; }
        public String DocumentID { get; set; }
        public String PersonName { get; set; }
    }
}