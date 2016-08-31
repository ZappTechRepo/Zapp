using Homemation.WebAPI.ActionFilters;
using Homemation.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TriboschAdmin;
using TriboschAdmin.Models;
using TriboschAdmin.WebAPI.Controllers;

namespace Homemation.WebAPI.Controllers
{
    //[APIAuthenticationFilter] 
    [AuthorizationRequired]
    [RoutePrefix("api/document")]
    public class DocumentController : BaseController
    {

        IDocumentRepository _repository;

        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository;
        }




        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage GetDocuments(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            List<Document> doc = _repository.GetAllDocuments(id);
            if (doc != null && doc.Count > 0)
                response = Request.CreateResponse(HttpStatusCode.OK, doc);

            return response;
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id:int}", Name = "GetDocumentsById")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("{status:alpha}", Name = "GetDocumentsByStatus")]
        public HttpResponseMessage Get(string status)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            List<Document> doc = _repository.GetDocumentByStatus(status);
            if (doc != null && doc.Count > 0)
               response = Request.CreateResponse( HttpStatusCode.OK, doc);

            return response;
        }


        [HttpGet]
        [Route("serialized", Name = "GetSerializedDocuments")]
        public HttpResponseMessage SerializedDocuments()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            List<Document> doc = _repository.GetAllDocuments(1);
            if (doc != null && doc.Count > 0)
                response = Request.CreateResponse(HttpStatusCode.OK, doc);

            return response;
            //var re = Request;
            //var headers = re.Headers;
            //string token = "";
            //if (headers.Contains("Token"))
            //{
            //    token = headers.GetValues("Token").First();
            //}


            ////test with http://localhost:8097/api/document/serialized
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);

            //int repguid = _repository.GetSalesRepGuidByToken(token);
            ////// List<DeliveryNote> del = _repository.GetDeliveryNoteDocuments(repguid);
            //// if (del != null && del.Count > 0)
            ////     response = Request.CreateResponse(HttpStatusCode.OK, del);

            //return response;
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage CompleteMyDelivery([FromBody]CompletedDelivery delivery)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            //test data
            //SignatureData sigdata1 = new SignatureData();
            //sigdata1.documentnumber = "WEB00018187";
            //sigdata1.base64img = "iVBORw0KGgoAAAANSUhEUgAAAGIAAAAtCAIAAADaySngAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAEjSURBVGhD7ZlBDoMwDAR5SI/8/2d9Q4tEFaEUyNoOqVUG9egkZrSTQJmej5lfk8DUrKBgIQAmSSYwganftkuaSBNpGnz+Ih3SId3dpXulvH6zN52gGBwTZbml20GYKi5Kc6lq+mPaTUqqe3Y00xPTCsjRRPIhnaX7V0b8kXL4bFm2jjXpPaVL7o7e3rcWYKoDtbt1gOmDqbKsih6YZuWAvjsm8XSesr1C6RttsFIJUVnClqZzgYN9r8Ovft3z3YINUwEhZlUEF090cyEfHWeamt24C0wKiKts6YtDjsqcaQquejQ8HquOaLZN5sJ0Ef34tGDikwGfDOIemWZAOqRDOpMy8WKkQzqki3tkmgHpkA7pTMrEi5EO6ZAu7pFpBqSTpHsDdAfA3rHzJHUAAAAASUVORK5CYII=";

            var re = Request;
            var headers = re.Headers;
            string token = "";
            if (headers.Contains("Token"))
            {
                token = headers.GetValues("Token").First();
            }


            //test with http://localhost:8097/api/document/serialized
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            string filepath = _repository.SaveSignature(delivery.Signature, delivery.DocumentID);

            if (filepath.IndexOf("ERROR") != -1)
            {
                response = Request.CreateResponse(HttpStatusCode.ExpectationFailed, filepath);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, filepath);
            }

            return response;
        }



        // POST api/<controller>
        [HttpPost]
        [Route("", Name = "CreateDocument")]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("", Name = "UpdateDocument")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("", Name = "DeleteDocument")]
        public void Delete(int id)
        {
        }
    }
}