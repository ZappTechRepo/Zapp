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
    
    public partial class Account
    {
        public Account()
        {
            this.PaymentOptions = new HashSet<PaymentOption>();
            this.ShippingOptions = new HashSet<ShippingOption>();
        }
    
        public long id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public System.Guid guid { get; set; }
        public byte[] checksum { get; set; }
        public string name { get; set; }
        public string accountNumber { get; set; }
        public string taxNumber { get; set; }
        public string websiteUrl { get; set; }
        public int status { get; set; }
        public long GroupId { get; set; }
        public Nullable<long> TemplateId { get; set; }
        public Nullable<System.Guid> SalesRepGuid { get; set; }
        public double TotalPurchases { get; set; }
        public double CurrentMonthPurchases { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public int ApprovalStatus { get; set; }
        public bool Can_Shop { get; set; }
        public bool DisplayXMLFeedLink { get; set; }
        public string PastelAccNumber { get; set; }
        public string PastelDiscount { get; set; }
        public string PastelPaymentType { get; set; }
        public Nullable<bool> VatInclusive { get; set; }
        public string ContactName { get; set; }
        public string PastelAddressCode { get; set; }
        public string PastelStoreName { get; set; }
        public string PastelDelivCode { get; set; }
        public string PastelDelivCodeName { get; set; }
    
        public virtual ICollection<PaymentOption> PaymentOptions { get; set; }
        public virtual ICollection<ShippingOption> ShippingOptions { get; set; }
    }
}
