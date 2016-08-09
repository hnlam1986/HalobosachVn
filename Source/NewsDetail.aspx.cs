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

namespace HaloBoSach
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        public string NewsUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            NewsUrl = Request.Url.ToString();

            if (IsPostBack)
                return;

            object _id = Page.RouteData.Values["id"];
            if (_id != null)
            {
                string id = _id.ToString();
                SqlParameter NewsId = new SqlParameter("NewsID", SqlDbType.Int);
                NewsId.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByNewID", NewsId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string NewsTitle = dr["NewsTitle"].ToString();
                    string NewsContent = dr["NewsContent"].ToString();
                    string SourceNews = dr["SourceNews"].ToString();
                    string catename = dr["NewsCateName"].ToString();
                    string postdate = dr["PostedDate"].ToString();
                    int cateid = int.Parse(dr["NewsCateID"].ToString());
                    string viewed = dr["Viewed"].ToString();
                    //NewsCateID = dr["NewsCateID"].ToString();
                    string ShortContent = dr["ShortContent"].ToString();
                    dvTitle.InnerText = NewsTitle;
                    this.Title = NewsTitle;
                    shortContent.InnerHtml = "<p>" + ShortContent + "</p>";
                    content.InnerHtml = NewsContent;
                    //source.InnerText = SourceNews;
                    lblFrom.Text = SourceNews;
                    lblDangluc.Text = postdate;
                    lblLuotXem.Text = viewed;
                    lnlTinLienQuan.CateID = cateid;
                    lnlTinLienQuan.NewID = int.Parse(id);


                    ListNewsLink1.CateID = cateid;
                    ListNewsLink1.NewID = int.Parse(id);
                }
            }
        }
    }
}