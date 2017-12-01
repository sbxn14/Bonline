using System;
using System.Web;
using System.Web.Security;

namespace Bonline.Models
{
 public class TicketAuth
 {
  public HttpCookie Encrypt(string id)
  {
   FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, id, DateTime.Now, DateTime.Now.Date.AddHours(3), false, "", FormsAuthentication.FormsCookiePath);
   HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
   return c;
  }

  public int Decrypt()
  {
   FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
   int key = Convert.ToInt32(ticket.Name);
   return key;
  }
 }
}