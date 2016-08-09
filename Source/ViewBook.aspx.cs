using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.entity;

namespace HaloBoSach
{
    public partial class ViewBook : System.Web.UI.Page
    {
        string bookFlipTemplate = @"<div id='myBook_{1}' style='position: fixed; top: 0px; left: 0px; z-index:99999'><div id='canvas'>
                                        <div class='magazine-viewport'> 
                                              <a class='close-book' onclick='parent.CloseBook();'><span  class='glyphicon glyphicon-remove-sign '></span></a>                                      
                                              <div class='container'>
		                                        <div class='magazine'>
                                                    
			                                        <!-- Next button -->
			                                        <div ignore='1' class='next-button'></div>
			                                        <!-- Previous button -->
			                                        <div ignore='1' class='previous-button'></div>
			                                        {0}
		                                        </div>
	                                        </div>
                                        </div></div></div>";
        string pageTemplate = "<div style='background-image:url({0})' {1}></div>";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
                return;
            int bookId = int.Parse(Request.QueryString["id"].ToString());
            List<AlbumEnt> collection = new List<AlbumEnt>();
            if (Session["BookCollection"] != null)
                collection = (List<AlbumEnt>)Session["BookCollection"];
            AlbumEnt book = collection.Where(p => p.AlbumId == bookId).FirstOrDefault();
            StringBuilder sbPage = new StringBuilder();
            int index = 0;

            foreach (AlbumDetailEnt ad in book.ListImage)
            {
                string bia = "";
                if (index < 1)
                {
                    bia = "class='hard'";


                }
                index++;
                string page = string.Format(pageTemplate, "/Albums/" + ad.ImagePath, bia);
                sbPage.Append(page);
                if (index == 1)
                {
                    page = string.Format(pageTemplate, "/ctrl/bookflip/pics/logo-black.png", "class='hard hard-logo'");
                    sbPage.Append(page);
                }
            }
            if ((book.ListImage.Count - 1) % 2 > 0)
            {
                string page = string.Format(pageTemplate, "/ctrl/bookflip/pics/blank.png", "");
                sbPage.Append(page);

            }
            //add trang bia sau;
            string strang_bia = string.Format(pageTemplate, "/ctrl/bookflip/pics/logo-black.png", "class='hard hard-logo'");
            sbPage.Append(strang_bia);
            sbPage.Append(strang_bia);

            string bookHtml = string.Format(bookFlipTemplate, sbPage.ToString(), book.AlbumId);
            bookContent.InnerHtml = bookHtml;
        }
    }
}