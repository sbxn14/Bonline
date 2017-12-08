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
            HttpCookie c = new HttpCookie("__RequestVerificationToken", FormsAuthentication.Encrypt(ticket));
            return c;
        }


        public int Decrypt()
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["__RequestVerificationToken"].Value);
            int key = Convert.ToInt32(ticket.Name);
            return key;
        }
    }
}