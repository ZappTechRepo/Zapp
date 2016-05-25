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
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Customer(Customer cust)
        {
            TriboschAppEntities entity = new TriboschAppEntities();

            return View(entity.Customers.ToList());
        }


        public ActionResult Product(Product products)
        {
            TriboschAppEntities entity = new TriboschAppEntities();
            
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

		public ActionResult Login()
		{
			return View("Login");
		}

	}
}