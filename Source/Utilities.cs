using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNet.FriendlyUrls;
using HaloBoSach.Dal;
using System.Text.RegularExpressions;
using HaloBoSach.entity;
using System.Web.SessionState;
using System.Configuration;
using System.Net.Mail;

namespace HaloBoSach
{
    public static class Utilities
    {
        public static string GetTempleAdvItem(bool isRefresh = false)
        {
            string template = "<td>" +
                        "<img src='/AdvImage/{0}' style='max-width:100px'/>" +
                        "</td>" +
                        //"<td>{1}</td>" +
                        //"<td>{2}</td>" +
                        "<td><a href=\"javascript:void(0);\" onclick=\"window.open('{3}');\">{3}</a></td>" +
                        "<td>" +
                        "<button type=\"button\" class=\"btn btn-warning btn-sm\" onclick=\"var popup = window.open('EditAdvertising.aspx?action=edit&id={4}','EditAdvWindow','toolbar=no, scrollbars=yes, resizable=yes, width=450px, height=450px');popup.focus();\">Edit</button>" +
                        "<button type=\"button\" class=\"btn btn-danger btn-sm\" onclick=\"AdvertisingManagementEvent.DeleteAdvItem('{4}','/AdvImage/{0}')\" style='margin-left:5px;'>Del</button>" +
                        "<img src='img/throbber.gif' id='adv_indicator_{4}' style='display:none'/>" +
                        "<a href=\"javascript:void(0);\" onclick=\"AdvertisingManagementEvent.RefreshAdvItem('{4}')\" id='refresh_adv_{4}' style='display:none'>Refresh</a>" +
                        "</td>";
            if(!isRefresh)
            {
                
                 template =    "<tr id='adv_{4}'>" + template+ "</tr>";
            }
            return template;
        }
        public static string GetFriendlyUrl(HttpRequest request,string page,params object[] id)
        {
            string link = FriendlyUrl.Href(page, id);
            return link;
        }
        public static string ConvertUnicodeToAscii(string uText)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = uText.Normalize(NormalizationForm.FormD);
            string res= regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return Regex.Replace(res.Replace(" ", "-"), "[^0-9a-zA-Z\\-]+", "").ToLower();
        }
        public static void WriteLog(string logDes)
        {
            SqlParameter param = new SqlParameter("LogDes",logDes);
            DataAccessLayer.ExcuteNoneQuery("Logs_InsertLog", param);
        }
        public static string BuildPaging(string pagingId, string jsHandler, int totalPage)
        {
            if (totalPage > 1)
            {
                string paing_template = "<ul id='" + pagingId + "' class='pgGrid'>{0}</ul>";
                string item_template = "<li><a {1} onclick='" + jsHandler + "(this,{2})'>{0}</a></li>";
                StringBuilder sbPaging = new StringBuilder();
                for (int i = 0; i < totalPage; i++)
                {
                    if (i == 0)
                    {
                        sbPaging.Append(string.Format(item_template, i + 1, "class='selected'", i + 1));
                    }
                    else
                    {
                        sbPaging.Append(string.Format(item_template, i + 1, "", i + 1));
                    }
                }
                string paging = string.Format(paing_template, sbPaging.ToString());
                return paging;
            }
            else
            {
                return "";
            }
        }
        public static ShoppingCart GetShopCartFromSession(HttpSessionState session)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            return cart;
        }
        public static string FormatCurrency(double number){
            return string.Format("{0:n0}", number) + "đ";
        }
        public enum SendTypeEmail{
            Contact,
            Order
        }
        public static bool SendEmail(SendTypeEmail type, string customerEmail, string subject, string content)
        {
            bool res = false;
            try
            {
                string defaultEmail = "";
                string sendEmail = "";
                string passEmail = "";
                if (ConfigurationManager.AppSettings["receiveEmail"] != null)
                {
                    defaultEmail = ConfigurationManager.AppSettings["receiveEmail"];
                    if(type == SendTypeEmail.Contact)
                        sendEmail = ConfigurationManager.AppSettings["sendEmailContact"];
                    else
                        sendEmail = ConfigurationManager.AppSettings["sendEmailOrder"];
                    passEmail = ConfigurationManager.AppSettings["passSendEmail"];
                }
                string email = customerEmail;
                if (email == "") 
                    email = defaultEmail;
                else
                    email = defaultEmail + ";" + customerEmail;
                MailMessage mail = new MailMessage(sendEmail, defaultEmail);
                string[] lstEmail = email.Split(';');
                foreach (string item in lstEmail)
                {
                    mail.To.Add(item);
                }
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential(sendEmail, passEmail);
                client.Host = "share06.vhost.vn";
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                client.Send(mail);
                res = true;
            }
            catch (Exception ex)
            {
                
            }
            return res;
        }
    }
}