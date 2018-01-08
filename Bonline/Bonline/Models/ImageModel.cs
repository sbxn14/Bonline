﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Bonline.Database;

namespace Bonline.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public bool IsSelected { get; set; }
        public string Info { get; set; }
    }
}