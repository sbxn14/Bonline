using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Bonline.Models
{
 public class PasswordManager
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
		 return hashString + "JYSI23DdsfUgdIGH3cjdfhraeiufhq37ryq73ufhkjrhfel292sefsda2dsDfgU65IH";
	 }

 }
}