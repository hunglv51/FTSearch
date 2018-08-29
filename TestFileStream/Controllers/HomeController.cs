using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestFileStream.Models;

namespace TestFileStream.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TestFileStream()
        {
            return View();
        }

        public IActionResult GetFile()
        {
            var connectionString = @"Data Source=DESKTOP-QH3V092\SQLEXPRESS;Initial Catalog=PDFDoc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adap = new SqlDataAdapter("select * from WordDocument where title like '%test1'",connection);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                DataTable dt = ds.Tables[0];

                if(dt != null && dt.Rows.Count > 0)
                {
                    byte[] byteImg = dt.Rows[0]["content"] as byte[];
                    return File(byteImg, "application/pdf");
                    //return new FileContentResult(byteImg, "image/png");
                }
                return NoContent();
            }

        }

        public IActionResult AddFile()
        {
            return View();
        }
    }
}
