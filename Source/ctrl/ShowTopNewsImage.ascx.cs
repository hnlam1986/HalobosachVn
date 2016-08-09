using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.ctrl
{
    public partial class ShowTopNewsImage : System.Web.UI.UserControl
    {
        public enum ShowNewsType{
            BigWideNewsTop,
            BigNewsTop,
            WideNewsTop
        }
        public bool ShowRightCaption { get; set; }
        public string GetRightCaptionCss
        {
            get
            {
                if (ShowRightCaption)
                {
                    return "right-caption";
                }
                return "";
            }
        }
        public int RecordNumber { get; set; }
        public int SmallNewsColumn { get; set; }
        public int GetCSSColumn { get {
            return 12 / SmallNewsColumn;
        } }
        public bool ShowBigNews { get; set; }
        public ShowNewsType ShowType { get; set; }
        public string GetCssOfType
        {
            get
            {
                if (ShowType == ShowNewsType.BigWideNewsTop)
                {
                    return "big-news-wide";
                }
                else if (ShowType == ShowNewsType.BigNewsTop)
                {
                    return "big-news-top";
                }
                else if (ShowType == ShowNewsType.WideNewsTop)
                {
                    return "wide-news-top";
                }
                return "";
            }
        }
        public string StoreProcedureName { get; set; }
        public string PositionKey { get; set; }
        public string RoutePath { get; set; }
        private bool odd = false;
        public string OddEven()
        {
            odd = !odd;
            if (odd)
                return "odd";
            else
                return "even";
            
        }
        public bool HideViewMoreButton { get; set; }
        public string GetImageUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url) ? "/NewsAvarta/" + url : "/Images/logo.png";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = null;
            if (PositionKey != "HotNews")
            {
                SqlParameter key = new SqlParameter("Key", PositionKey);
                SqlParameter record = new SqlParameter("NumOfRecord", RecordNumber);
                ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName, key, record);
            }
            else
            {
                ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName);
            }
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string image = ds.Tables[0].Rows[0]["ThumnailImagePath"].ToString();
                string shortContent = ds.Tables[0].Rows[0]["ShortContent"].ToString();
                string title = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
                string id = ds.Tables[0].Rows[0]["NewsID"].ToString();
                string cate_id = ds.Tables[0].Rows[0]["ParentID"].ToString();
                string url = "/" + RoutePath + "/" + id + "/" + Utilities.ConvertUnicodeToAscii(title) + ".halobosach.vn";
                string imgUrl = !string.IsNullOrWhiteSpace(image) ? "/NewsAvarta/" + image : "/Images/logo.png";
                Image1.ImageUrl = imgUrl;
                Image2.ImageUrl = imgUrl;
                firstNewsLink.HRef = url;
                firstNewsLinkWide.HRef = url;
                firstDescription.InnerHtml = "<span class='first-new-title'>" + title + "</span>" + "<br/>" + shortContent;
                firstDescriptionWide.InnerHtml = "<span class='first-new-title'>" + title + "</span>" + "<br/>" + shortContent;
                moreNewsLink.HRef = "/tin-tuc/xem-tat-ca/" + cate_id;
                DataRow dr = ds.Tables[0].Rows[0];
                dr.Delete();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                Image1.Visible = false;
                Image2.Visible = false;
                divShowTopNewsImage.Visible = false;
            }
        }
    }
}