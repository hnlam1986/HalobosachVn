using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;
using HaloBoSach.entity;

namespace HaloBoSach.ctrl.bookflip
{
    public partial class bookflip : System.Web.UI.UserControl
    {
        string bookFlipTemplate = @"<div id='myBook_{1}' class='fade hide' style='position: fixed; top: 0px; left: 0px; z-index:99999'><div id='canvas'>
                                        <div class='magazine-viewport'>
                                            <a class='close-book' onclick='CloseBook()'><span  class='glyphicon glyphicon-remove-sign '></span></a>
	                                        
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
            Page.ClientScript.RegisterClientScriptInclude("key0", "/Scripts/jquery.blockUI.js");
            Page.ClientScript.RegisterClientScriptInclude("key", "/ctrl/bookflip/extras/modernizr.2.5.3.min.js");
            Page.ClientScript.RegisterClientScriptInclude("key1", "/ctrl/bookflip/lib/hash.js");
            
            if (IsPostBack)
                return;

            DataSet ds = DataAccessLayer.ExecuteDataSet("Cooking_LoadViewAllBookPage");
            List<AlbumEnt> lstAlbum = new List<AlbumEnt>();
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int AlbumId = int.Parse(row["AlbumId"].ToString());
                    string AlbumName = row["AlbumTitle"].ToString();
                    string FolderPath = row["FolderPath"].ToString();
                    string ImageUrl = row["ImageUrl"].ToString();
                    int OrderImage = int.Parse(row["OrderImage"].ToString());
                    AlbumEnt album = lstAlbum.Where(p => p.AlbumId == AlbumId).FirstOrDefault();
                    if (album != null)
                    {
                        AlbumDetailEnt detail = new AlbumDetailEnt();
                        detail.ImagePath = FolderPath + "/" + ImageUrl;
                        detail.OrderImage = OrderImage;
                        if (album.ListImage == null)
                        {
                            album.ListImage = new List<AlbumDetailEnt>();
                        }
                        album.ListImage.Add(detail);
                    }
                    else
                    {
                        AlbumEnt album1 = new AlbumEnt();
                        album1.AlbumId = AlbumId;
                        album1.AlbumName = AlbumName;
                        album1.FolderPath = FolderPath;
                        album1.ListImage = new List<AlbumDetailEnt>();
                        AlbumDetailEnt detail = new AlbumDetailEnt();
                        detail.ImagePath = FolderPath + "/" + ImageUrl;
                        detail.OrderImage = OrderImage;
                        album1.ListImage.Add(detail);
                        lstAlbum.Add(album1);
                    }
                }
            }
            if (lstAlbum != null && lstAlbum.Count > 0)
            {
                Session["BookCollection"] = lstAlbum;
                StringBuilder sb = new StringBuilder();

                StringBuilder sbBook = new StringBuilder();
                sb.Append("<ul class='book-list'>");
                string liTemplate = "<li class='col-sm-3 text-center'><a onclick='OpenBook({2});'><div class='book-outbound'><img src='/Albums/{0}' alt='{1}' class='book'/></div></a></li>";
                foreach (AlbumEnt album in lstAlbum)
                {
                    string li = string.Format(liTemplate, album.GetFirstImage, album.AlbumName, album.AlbumId);
                    sb.Append(li);
                    StringBuilder sbPage = new StringBuilder();
                    int index = 0;
                    foreach (AlbumDetailEnt ad in album.ListImage)
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
                    if ((album.ListImage.Count-1) % 2 > 0)
                    {
                        string page = string.Format(pageTemplate, "/ctrl/bookflip/pics/blank.png", "");
                        sbPage.Append(page);

                    }
                    //add trang bia sau;
                    string strang_bia = string.Format(pageTemplate, "/ctrl/bookflip/pics/logo-black.png", "class='hard hard-logo'");
                    sbPage.Append(strang_bia);
                    sbPage.Append(strang_bia);

                    string book = string.Format(bookFlipTemplate, sbPage.ToString(), album.AlbumId);
                    sbBook.Append(book);
                }
                sb.Append("</ul>");
                bookSheftContent.InnerHtml = sb.ToString();
                //bookFlipContent.InnerHtml = sbBook.ToString();
            }


        }
    }
}