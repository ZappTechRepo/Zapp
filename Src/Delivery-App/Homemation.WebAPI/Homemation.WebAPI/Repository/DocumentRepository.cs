using System;
using System.Collections.Generic;
using System.Linq;

namespace Homemation.WebAPI.Models
{
    public class DocumentRepository : IDisposable, IDocumentRepository
    {
        private EntitiesConnectionA dataContext = new EntitiesConnectionA();

        public List<Document> GetAllDocuments()
        {
            return new List<Document>();
        }

        public List<Document> SearchDocumentByName( string employeeName )
        {
            return new List<Document>();
        }

        public List<Document> GetDocumentByStatus( string pStatus )
        {
            Guid gStatusGuid = GetDocumentStatusTypeGuid( pStatus );
            List<Document> doc = dataContext.Documents.Where( d => d.DocumentStatusGuid.Equals( gStatusGuid ) ).ToList();

            return doc; //doc != null ? doc : new List<Document>();
        }

        public Guid GetSalesRepGuidByCode(string repcode)
        {
            Guid sRepGuid = Guid.Empty;
            SalesRep rep =  dataContext.SalesReps.FirstOrDefault(rp => rp.SalesRepCode.Equals(repcode));
           if (rep != null)
            {
                sRepGuid = rep.rowguid;
            }

            return sRepGuid;
        }


        public Guid GetSalesRepGuidByToken(string token)
        {
            TokenSaleRep tonkenrep = dataContext.TokenSaleReps.FirstOrDefault(ts => ts.AuthToken.ToUpper().Equals(token.ToUpper()));

            Guid gRepGuid = Guid.Empty;
            if (tonkenrep != null)
            {
          
                gRepGuid = tonkenrep.UserGuid;

            }

            return gRepGuid;
        }


        public List<DeliveryNote> GetDeliveryNoteDocuments(Guid UserId)
        {

            if (UserId == Guid.Empty)
            {
                return new List<DeliveryNote>();
            }
            
        


            List<DeliveryNote> deliveryorders = dataContext.Database.SqlQuery<DeliveryNote>("SP_GetDeliveryNote @UserGuid = {0}", UserId).ToList();

                foreach (DeliveryNote deliveryNote in deliveryorders)
                {
                    deliveryNote.deliveryNoteLines = new List<DeliveryNoteLine>();
                    List<DeliveryNoteLine> deliverynotelines = dataContext.Database.SqlQuery<DeliveryNoteLine>("SP_GetDeliveryNoteLines @DocGuid = {0}", deliveryNote.DocGuid).ToList();
                    foreach (DeliveryNoteLine dlline in deliverynotelines)
                    {
                        dlline.Pkg = "";
                        deliveryNote.deliveryNoteLines.Add(dlline);
                        //item.deliveryNoteLines.Add(new DeliveryNoteLine { Code = dlline.Code, Pkg = "", Product = dlline.Product, Quantity = dlline.Quantity, Serials = dlline.Serials });
                    }

                    GetGeoCodedAddress(deliveryNote);
                }
           

            return deliveryorders;
        }

        private void GetGeoCodedAddress( DeliveryNote deliveryNote )
        {
            Int32 count = 0;
            Geocoding.Google.GoogleGeocoder geocoder = new Geocoding.Google.GoogleGeocoder() { };
            if ( !String.IsNullOrEmpty( System.Configuration.ConfigurationManager.AppSettings[ "GoogleApiKey" ] ) )
                geocoder.ApiKey = System.Configuration.ConfigurationManager.AppSettings[ "GoogleApiKey" ];
            while ( String.IsNullOrEmpty( deliveryNote.FormattedShippingAddress ) && count < 6 )
            {
                String Address = JoinAddress( deliveryNote.shippingAddress_1, deliveryNote.shippingAddress_2, deliveryNote.shippingAddress_3, deliveryNote.shippingAddress_4, deliveryNote.shippingAddressCountry, deliveryNote.shippingAddressCode, count );
                try
                {
                    Geocoding.Address address = geocoder.Geocode( Address ).FirstOrDefault();
                    if ( address == null )
                    {
                        count++;
                        continue;
                    }

                    deliveryNote.FormattedShippingAddress = address.FormattedAddress;
                    deliveryNote.ShippingAddressCoordinates = new Location { Latitude = address.Coordinates.Latitude, Longitude = address.Coordinates.Longitude };
                }
                catch ( Exception ex )
                {
                    count++;
                }
            }
        }

        private string JoinAddress( string Address_1, string Address_2, string Address_3, string Address_4, string shippingAddressCountry, string shippingAddressCode, Int32 count )
        {
            String addr = "";

            if ( count == 0 || count > 3 )
            {
                addr += ( Address_1.Length > 0 ? Address_1 : "" );
            }
            if ( count < 2 || count == 4 || count == 5 )
            {
                addr += ( ( addr.Length > 0 ? " " : "" ) + ( Address_2.Length > 0 ? Address_2 : "" ) );
            }
            if ( count < 3 || count == 4 )
            {
                addr += ( ( addr.Length > 0 ? " " : "" ) + ( Address_3.Length > 0 ? Address_3 : "" ) );
            }
            if ( count < 4 )
            {
                addr += ( ( addr.Length > 0 ? " " : "" ) + ( Address_4.Length > 0 ? Address_4 : "" ) );
            }

            return addr + " " + shippingAddressCountry;
        }

        private Guid GetDocumentStatusTypeGuid( string pStatus )
        {
            Guid gStatusGuid = Guid.Empty;
            DocumentStatu docstatus = dataContext.DocumentStatus.FirstOrDefault( ds => ds.Name.Equals( pStatus ) );
            if ( docstatus != null )
            {
                gStatusGuid = docstatus.Rowguid;
            }

            return gStatusGuid;
        }

        protected void Dispose( bool disposing )
        {
            if ( disposing )
            {
                if ( dataContext != null )
                {
                    dataContext.Dispose();
                    dataContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
    }
}