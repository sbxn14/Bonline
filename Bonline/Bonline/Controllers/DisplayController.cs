using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Bonline.Models;



namespace Bonline.Controllers
{
    public class DisplayController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<ImageModel> images = GetImages();
            return View(images);
        }

        [HttpPost]
        public ActionResult Index(int imageId)
        {
            List<ImageModel> images = GetImages();
            ImageModel image = images.Find(p => p.Id == imageId);
            if (image != null)
            {
                image.IsSelected = true;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
            }
            return View(images);
        }

        private List<ImageModel> GetImages()
        {
            string query = "SELECT * FROM tblFiles";
            List<ImageModel> images = new List<ImageModel>();
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            images.Add(new ImageModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["Name"].ToString(),
                                Info = sdr["Info"].ToString(),
                                ContentType = sdr["ContentType"].ToString(),
                                Data = (byte[])sdr["Data"]

                            });
                        }
                    }
                    con.Close();
                }

                return images;
            }
        }
    }
}