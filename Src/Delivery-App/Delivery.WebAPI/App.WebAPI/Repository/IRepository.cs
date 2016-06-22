using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homemation.WebAPI.Models
{


    public interface IAccountRepository
    {
        List<Customer> GetAllCustomer();
        List<Customer> SearchCustomerByName(string employeeName);
        Customer LoginCustomer(CustomerLogin cl);
        Customer GetCustomer(int CustomerID);
        Customer GetLogCustomer(int CustomerID);
        List<Customer> InsertCustomer(Customer e);
        List<Customer> UpdateCustomer(Customer e);
        List<Customer> DeleteCustomer(Customer e);
    }



    public interface IDocumentRepository
    {

        List<Document> GetAllDocuments();
        List<Document> SearchDocumentByName(string employeeName);
        List<Document> GetDocumentByStatus(string pStatus);
        List<DeliveryNote> GetDeliveryNoteDocuments(Guid UserId);
        Guid GetSalesRepGuidByCode(string repcode);
        Guid GetSalesRepGuidByToken(string token);





    }



}