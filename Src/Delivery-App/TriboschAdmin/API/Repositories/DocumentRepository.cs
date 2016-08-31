using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using TriboschAdmin;

namespace Homemation.WebAPI.Models
{
    public class DocumentRepository : IDisposable, IDocumentRepository
    {
        private TriboschAppEntities  dataContext = new TriboschAppEntities();

        private static String SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
        private static Int32 SMTPPort = Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
        private static String SMTPUsername = ConfigurationManager.AppSettings["SMTPUsername"];
        private static String SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"];

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

                    SendDeliveryMail(doc);
                }
            }
            catch (Exception ex)
            {
                svgFileName = "ERROR:" + ex.Message.ToString();
            }

            return svgFileName;
        }

        public void SendDeliveryMail(Document doc)
        {
            try
            {
                String body = string.Empty;
                string html = File.ReadAllText(ConfigurationManager.AppSettings["MailTemplate"] + "/DeliveryNote.html");
                string Lines = string.Empty;
                Address addr = doc.Customer.Addresses.First();

                html = html.Replace("#DATE#", DateTime.Now.ToString(" dd MMM yyyy"));
                html = html.Replace("#ADDRESS#", addr.Address1 + "<br/>" + addr.Address2 + "<br/>" + addr.Address3 + "<br/>" + addr.PostalCode);
                html = html.Replace("#NO#", doc.InvoiceNo);

                foreach (Line ln in doc.Lines)
                {
                    Lines += @"<tr><td><p>" + ln.Qty + "</p></td>" +
                                "<td><p>" + ln.Product.ProductName + "</p></td>" +
                                "<td><p>" + ln.Product.RetailPriceIncl + "</p></td></tr>";
                }

                html = html.Replace("#LINES#", Lines);
                html = html.Replace("#SUBTOTAL#", "R " + doc.TotalExcl.ToString());
                html = html.Replace("#TAX#", "R " + (doc.TotalExcl * 1.14).ToString());
                html = html.Replace("#TOTAL#", "R " + doc.TotalIncl.ToString());

                body = html;

                SendMail("Tribosch Delivery Completed", body, doc.Customer.CustomerName, doc.Customer.Email, "kb.kwena@gmail.com");
            }
            catch (Exception ex)
            {
                
            }
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

        public static void SendMail(String subject, String body, String fromName, String replyAddress, String recipientAddress)
        {
            //Send the email
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = SMTPHost;
                smtp.Port = SMTPPort;
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                using (MailMessage msg = new MailMessage())
                {
                    msg.Subject = subject;
                    msg.From = new MailAddress(replyAddress, fromName);
                    msg.IsBodyHtml = true;
                    msg.Body = body;
                    msg.To.Add(new MailAddress(recipientAddress));

                    smtp.Send(msg);
                }
            }
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