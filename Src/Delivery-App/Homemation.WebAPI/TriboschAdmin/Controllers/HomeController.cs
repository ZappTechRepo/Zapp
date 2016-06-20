using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TriboschAdmin.Controllers
{
	public class HomeController : Controller
	{
        TriboschAppEntities entity = new TriboschAppEntities();
        public ActionResult Index()
		{
			return View();
		}

        #region Documents
        public ActionResult Documents(Document doc)
        {
            return View(entity.Documents.ToList());
        }

        public ActionResult EditDocuments(int id = 0)
        {
            Document doc = entity.Documents.Find(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(doc);
        }

        [HttpPost]
        public ActionResult EditDocuments(Document doc)
        {
            if (ModelState.IsValid)
            {
                entity.Entry(doc).State = System.Data.Entity.EntityState.Modified;
                foreach (Line adr in doc.Lines)
                {
                    entity.Entry(adr).State = System.Data.Entity.EntityState.Modified;
                }
                entity.SaveChanges();
                return RedirectToAction("Documents");
            }
            return View(doc);
        }


        public ActionResult CreateDocument()
        {
            ViewBag.Customer = new SelectList(entity.Customers, "CustomerId", "CustomerName");
            ViewBag.Product = new SelectList(entity.Products, "id", "Productname");
            //var tuple = new Tuple<Customer, Document>();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "LineID,ProductId,QTY,TotalExcl,TotalIncl,docID")] Line line)
        {
            if (ModelState.IsValid)
            {
                entity.Lines.Add(line);
                entity.SaveChanges();
                return RedirectToAction("Index", "CompanyEmployee");
            }

            return View(line);
        }

        public PartialViewResult AddOrderLines()
        {
            var model = new Line();
            return PartialView("_OrderLines");
        }

        [HttpPost]
        public ActionResult CreateDocument(Document doc)
        {

            if (ModelState.IsValid)
            {
                entity.Entry(doc).State = System.Data.Entity.EntityState.Modified;
                foreach (Line line in doc.Lines)
                {
                    entity.Entry(line).State = System.Data.Entity.EntityState.Modified;
                }
                entity.Documents.Add(doc);
                entity.SaveChanges();

                return RedirectToAction("Documents");
            }
            else
            {
                return View(doc);
            }
        }
        #endregion

        #region Customer
        public ActionResult Customer(Customer cust)
        {
           return View(entity.Customers.ToList());
        }

        public ActionResult EditCustomer(int id = 0)
        {
            Customer cust = entity.Customers.Find(id);
            if (cust == null)
            {
                return HttpNotFound();
            }
            return View(cust);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                entity.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                foreach (Address adr in customer.Addresses)
                {
                    entity.Entry(adr).State = System.Data.Entity.EntityState.Modified;
                }
                entity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Create Customer
        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {

            if (ModelState.IsValid)
            {
                entity.Customers.Add(customer);
                entity.SaveChanges();

                return RedirectToAction("Customer");
            }
            else
            {
                return View(customer);
            }
        }
        #endregion


        #region Products
        public ActionResult EditProducts(int id = 0)
        {
            Product prod = entity.Products.Find(id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            return View(prod);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult EditProducts(Product prod)
        {
            if (ModelState.IsValid)
            {
                entity.Entry(prod).State = System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        public ActionResult Product(Product products)
        {
            return View(entity.Products.ToList());
        }


        [HttpPost]
        public ActionResult Product(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string conn = ConfigurationManager.ConnectionStrings["Tribosch"].ConnectionString;
                    SqlConnection con = new SqlConnection(conn);
                    string query = "Insert into Products(ProductName,PackSize,PriceExcl,RetailPriceExcl,RetailPriceIncl,Qty) Values('" +
                        ds.Tables[0].Rows[i][0].ToString() +
                        "','" + ds.Tables[0].Rows[i][1].ToString() +
                        "','" + ds.Tables[0].Rows[i][2].ToString() +
                        "','" + ds.Tables[0].Rows[i][3].ToString() +
                        "','" + ds.Tables[0].Rows[i][4].ToString() +
                        "','" + ds.Tables[0].Rows[i][5].ToString() + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            TriboschAppEntities entity = new TriboschAppEntities();

            return View(entity.Products.ToList());
        }
        #endregion


        #region standard Constructors
        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }
        #endregion


        public ActionResult Login()
		{
			return View("Login");
		}

	}
}