//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homemation.WebAPI.Models
{
    using System;
    
    public partial class SP_GetDeliveryNote_Result
    {
        public string OrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> DeliveryNoteDate { get; set; }
        public string Customer { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderComments { get; set; }
        public string shippingAddress_1 { get; set; }
        public string shippingAddress_2 { get; set; }
        public string shippingAddress_3 { get; set; }
        public string shippingAddress_4 { get; set; }
        public string shippingAddressCode { get; set; }
        public string shippingAddressCountry { get; set; }
        public string billingAddress_1 { get; set; }
        public string billingAddress_2 { get; set; }
        public string billingAddress_3 { get; set; }
        public string billingAddress_4 { get; set; }
        public string billingAddressCode { get; set; }
        public string billingAddressCountry { get; set; }
    }
}
