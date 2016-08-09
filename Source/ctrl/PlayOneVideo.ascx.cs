using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using HaloBoSach.Dal;

namespace HaloBoSach.ctrl
{
    public partial class PlayOneVideo : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string ParentID { get; set; }
        public string CateID { get; set; }
        public string GetImageSource(string value)
        {
            if (value != null && value.Trim().Length > 0)
            {
                if (value.ToString().ToLower().Contains("youtube.com/watch?v="))
                {
                    string videoID = value.ToString().Substring(value.ToString().ToLower().LastIndexOf("?v=") + 3);
                    string img = "http://img.youtube.com/vi/" + videoID + "/1.jpg";
                    return img;
                }
                return value;
            }
            return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> lst = Request.GetFriendlyUrlSegments();
            string ID = "";
            SqlParameter key = null;
            DataSet ds = null;
            if (lst.Count >= 2)
            {
                ID = lst[1];
                key = new SqlParameter("CateID", ID);
                ds = DataAccessLayer.ExecuteDataSet("Video_Get11VideoByCateID", key);
            }
            else
            {
                ID = Request.QueryString["id"] ?? "0";
                key = new SqlParameter("VideoID", ID);
                ds = DataAccessLayer.ExecuteDataSet("Video_Get11VideoByVideoID", key);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string source = ds.Tables[0].Rows[0]["SourceURL"].ToString();
                string title = ds.Tables[0].Rows[0]["VideoTitle"].ToString();
                string id = ds.Tables[0].Rows[0]["VideoID"].ToString();
                string titlebar = ds.Tables[0].Rows[0]["NewsCateName"].ToString();
                Title = titlebar;
                //Image1.ImageUrl = GetImageSource(source);
                Uri uri = new Uri(source);
                string play = "http://www.youtube.com/v/" + uri.Query.Split('=')[1];
                lblVideo.Text =
                    "<object ><param name='movie' value='" + play +
                    "'></param><param name='wmode' value='transparent'></param><param name=\"allowFullScreen\" value=\"true\"></param><embed src='" +
                    play +
                    "' type='application/x-shockwave-flash' wmode='transparent' width='530px' height='369px' allowfullscreen=\"true\"></embed></object>";
                
                topTitle.InnerText = title;
                DataRow dr = ds.Tables[0].Rows[0];
                dr.Delete();
                int index = 0;
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}