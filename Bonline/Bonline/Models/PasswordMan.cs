using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Bonline.Models
{
 public class PasswordMan
 {
	 public static string Hash(string password)
	 {
		 byte[] bytes = Encoding.Unicode.GetBytes(password);
		 SHA256Managed hashstring = new SHA256Managed();
		 byte[] hash = hashstring.ComputeHash(bytes);
		 string hashString = string.Empty;
		 foreach (byte x in hash)
		 {
			 hashString += String.Format("{0:x2}", x);
		 }
		 return hashString + "supernaturalsaltkeepsthedemonsout";
	 }

 }
}