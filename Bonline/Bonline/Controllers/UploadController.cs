﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using Bonline.Models;

namespace Bonline.Controllers
{ 
    public class UploadController : Controller

{
    // GET: Home
    public ActionResult Index()
    {
        return View(GetFiles());
    }

    [HttpPost]
    public ActionResult Index(HttpPostedFileBase postedFile)
    {
        byte[] bytes;
        using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        {
            bytes = br.ReadBytes(postedFile.ContentLength);
        }
        string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "INSERT INTO tblFiles (Name, ContentType, Data) VALUES (@Name, @ContentType, @Data)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                cmd.Parameters.AddWithValue("@Data", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        return View(GetFiles());
    }

    [HttpPost]
    public FileResult DownloadFile(int? fileId)
    {
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT Name, Data, ContentType FROM tblFiles WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", fileId);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                con.Close();
            }
        }

        return File(bytes, contentType, fileName);
    }

    private static List<FileModel> GetFiles()
    {
        List<FileModel> files = new List<FileModel>();
        string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM tblFiles"))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        files.Add(new FileModel
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = sdr["Name"].ToString()
                        });
                    }
                }
                con.Close();
            }
        }
        return files;
    }
}
}
