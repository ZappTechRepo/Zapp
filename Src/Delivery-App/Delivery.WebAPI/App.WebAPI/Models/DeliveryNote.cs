using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homemation.WebAPI.Models
{
    public class DeliveryNote
    {
        public Guid DocGuid  { get; set; }
        public String OrderNumber { get; set; }
        public String InvoiceNumber { get; set; }
        public String DeliveryNoteDate { get; set; }
        public String Customer { get; set; }
        public String AccountNumber { get; set; }
        public String CustomerPhone { get; set; }
        public String CustomerOrderNumber { get; set; }

        public String OrderComments { get; set; }
        
        public String shippingAddress_1 { get; set; }
        public String shippingAddress_2 { get; set; }
        public String shippingAddress_3 { get; set; }
        public String shippingAddress_4 { get; set; }
        public String shippingAddressCode { get; set; }
        public String shippingAddressCountry { get; set; }

        public String billingAddress_1 { get; set; }
        public String billingAddress_2 { get; set; }
        public String billingAddress_3 { get; set; }
        public String billingAddress_4 { get; set; }
        public String billingAddressCode { get; set; }
        public String billingAddressCountry { get; set; }

        public String FormattedShippingAddress { get; set; }
        public Location ShippingAddressCoordinates { get; set; }


        public String DeliveryPerson { get; set; }
        public Guid DeliveryGuid { get; set; }
        public String ConsigmentID { get; set; }


        public List<DeliveryNoteLine> deliveryNoteLines { get; set; }

    }

    public class Location
    {
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }

    public class DeliveryNoteLine
    {

        public String Code { get; set; }
        public String Product { get; set; }
        public Double Quantity { get; set; }
        public String Pkg { get; set; }
        public String Serials { get; set; }

    }


}