using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TriboschAdmin;

namespace Homemation.WebAPI.Models
{
    public class DocumentRepository : IDisposable, IDocumentRepository
    {
        private TriboschAppEntities  dataContext = new TriboschAppEntities();

        public List<Document> GetAllDocuments()
        {
            return new List<Document>();
        }

        public List<Document> SearchDocumentByName( string employeeName )
        {
            return new List<Document>();
        }

        //public List<Document> GetDocumentByStatus( string pStatus )
        //{
        //    //Guid gStatusGuid = GetDocumentStatusTypeGuid( pStatus );
        //    //List<Document> doc = dataContext.Documents.Where( d => d.DocumentStatusGuid.Equals( gStatusGuid ) ).ToList();

        //    //return doc; //doc != null ? doc : new List<Document>();
        //}

        public Guid GetSalesRepGuidByCode(string repcode)
        {
            throw new NotImplementedException();
            // Guid sRepGuid = Guid.Empty;
            // SalesRep rep =  dataContext.SalesReps.FirstOrDefault(rp => rp.SalesRepCode.Equals(repcode));
            //if (rep != null)
            // {
            //     sRepGuid = rep.rowguid;
            // }

            // return sRepGuid;
        }




        //public List<DeliveryNote> GetDeliveryNoteDocuments(Guid UserId)
        //{

        //    if (UserId == Guid.Empty)
        //    {
        //        return new List<DeliveryNote>();
        //    }




        //    List<DeliveryNote> deliveryorders = dataContext.Database.SqlQuery<DeliveryNote>("SP_GetDeliveryNote @UserGuid = {0}", UserId).ToList();

        //        foreach (DeliveryNote deliveryNote in deliveryorders)
        //        {
        //            deliveryNote.deliveryNoteLines = new List<DeliveryNoteLine>();
        //            List<DeliveryNoteLine> deliverynotelines = dataContext.Database.SqlQuery<DeliveryNoteLine>("SP_GetDeliveryNoteLines @DocGuid = {0}", deliveryNote.DocGuid).ToList();
        //            foreach (DeliveryNoteLine dlline in deliverynotelines)
        //            {
        //                dlline.Pkg = "";
        //                deliveryNote.deliveryNoteLines.Add(dlline);
        //                //item.deliveryNoteLines.Add(new DeliveryNoteLine { Code = dlline.Code, Pkg = "", Product = dlline.Product, Quantity = dlline.Quantity, Serials = dlline.Serials });
        //            }

        //            GetGeoCodedAddress(deliveryNote);
        //        }


        //    return deliveryorders;
        //}

        private void GetGeoCodedAddress(Customer cust)
        {
            Int32 count = 0;
            Geocoding.Google.GoogleGeocoder geocoder = new Geocoding.Google.GoogleGeocoder() { };
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["GoogleApiKey"]))
                geocoder.ApiKey = System.Configuration.ConfigurationManager.AppSettings["GoogleApiKey"];

            String Address = JoinAddress(cust.Addresses.First().Address1, cust.Addresses.First().Address2, cust.Addresses.First().Address3, cust.Addresses.First().Country, cust.Addresses.First().PostalCode);
                try
            {
                    Geocoding.Address address = geocoder.Geocode(Address).FirstOrDefault();
                  //  deliveryNote.ShippingAddressCoordinates = new Location { Latitude = address.Coordinates.Latitude, Longitude = address.Coordinates.Longitude };
                }
                catch (Exception ex)
                {
                    count++;
                }
        }

        private string JoinAddress( string Address_1, string Address_2, string Address_3, string shippingAddressCountry, string shippingAddressCode)
        {
            String addr = "";
            addr = Address_1 + " " + Address_2 + " " + Address_3;
           return addr + " " + shippingAddressCountry;
        }

        //private Guid GetDocumentStatusTypeGuid( string pStatus )
        //{
        //    Guid gStatusGuid = Guid.Empty;
        //    DocumentStatu docstatus = dataContext.DocumentStatus.FirstOrDefault( ds => ds.Name.Equals( pStatus ) );
        //    if ( docstatus != null )
        //    {
        //        gStatusGuid = docstatus.Rowguid;
        //    }

        //    return gStatusGuid;
        //}

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

        List<Document> IDocumentRepository.GetAllDocuments(int UserId)
        {
            return dataContext.Documents.Where(doc => doc.UserId == UserId).ToList();
        }
             
        List<Document> IDocumentRepository.SearchDocumentByName(string employeeName)
        {
            throw new NotImplementedException();
        }

        public List<Document> GetDocumentByStatus(string pStatus)
        {
            throw new NotImplementedException();
        }

        //public List<DeliveryNote> GetDeliveryNoteDocuments(Guid UserId)
        //{
        //    throw new NotImplementedException();
        //}

        public string SaveSignature(string base64Img, string documentNumber)
        {

            string svgFileName = "";//svgFileName + ".PNG";
            int docid = Int32.Parse(documentNumber);

            try
            {

                Document doc = dataContext.Documents.FirstOrDefault(p => p.ID.Equals(docid));
                if (doc != null)
                {
                    byte[] sigbytes = Convert.FromBase64String(base64Img);


                    string SignatureUploadPath = System.Configuration.ConfigurationManager.AppSettings["SignatureUploadPath"];

                    if (!Directory.Exists(SignatureUploadPath + "/" + doc.ID.ToString().ToUpper() + "/Delivery Signature"))
                        Directory.CreateDirectory(SignatureUploadPath + "/" + doc.ID.ToString().ToUpper() + "/Delivery Signature");

                    svgFileName = SignatureUploadPath + "/" + doc.ID.ToString().ToUpper() + "/Delivery Signature/DS" + documentNumber + ".PNG";

                    Image img = LoadImage(sigbytes);

                    img.Save(svgFileName);


                    doc.SIgnature = sigbytes;

                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                svgFileName = "ERROR:" + ex.Message.ToString();
            }

            return svgFileName;
        }



        public Image LoadImage(byte[] img64)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = img64; //"R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        int IDocumentRepository.GetSalesRepGuidByToken(string token)
        {
            TriboschAdmin.TokenSaleRep tonkenrep = dataContext.TokenSaleReps.FirstOrDefault(ts => ts.AuthToken.Equals(token));

            int gRepGuid = 0;
            if (tonkenrep != null)
            {
                gRepGuid = Convert.ToInt32(tonkenrep.UserId);
            }

            return gRepGuid;
        }
    }
}