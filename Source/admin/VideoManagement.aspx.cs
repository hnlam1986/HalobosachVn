using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class VideoManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GetVideo();
            GetCategory();
            if(Request.QueryString["id"]!=null)
            {
                Button1.Visible = true;
                string id = Request.QueryString["id"] ?? "0";
                btnSubmitTop.Text = "Update Video";
                SqlParameter pID = new SqlParameter("VideoID", SqlDbType.Int);
                pID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Video_LoadVideoByID", pID);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string title = ds.Tables[0].Rows[0]["VideoTitle"].ToString();
                    string link = ds.Tables[0].Rows[0]["SourceURL"].ToString();
                    bool isHot =
                        bool.Parse(ds.Tables[0].Rows[0]["HotVideo"] != null
                                       ? ds.Tables[0].Rows[0]["HotVideo"].ToString()
                                       : "False");
                    string cateid = ds.Tables[0].Rows[0]["NewsCateID"] == null ? "0" : ds.Tables[0].Rows[0]["NewsCateID"].ToString();
                    txtTitle.Text = title;
                    txtLink.Text = link;
                    chkHot.Checked = isHot;
                    ListItem selectedListItem = DropDownList1.Items.FindByValue(cateid);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                }
            }
            else
            {
                Button1.Visible = false;
            }
        }
        private void GetCategory()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Video_LoadVideoCategory");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList1.Items.Add(new ListItem(text, id, true));
                }
            }
        }
        public string GetYoutubeUrl(string url)
        {
            Uri uri = new Uri(url);
            string play = "http://www.youtube.com/v/" + uri.Query.Split('=')[1];
            return play;
        }
        private void GetVideo()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Video_GetAllVideo");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                 
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
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
        protected void btnSubmitTop_OnClick(object sender, EventArgs e)
        {
            SqlParameter pTitle = new SqlParameter("VideoTitle", SqlDbType.NVarChar);
            pTitle.Value = txtTitle.Text;
            SqlParameter pLink = new SqlParameter("SourceURL", SqlDbType.NVarChar);
            pLink.Value = txtLink.Text;
            SqlParameter pHot = new SqlParameter("HotVideo", SqlDbType.Bit);
            pHot.Value = chkHot.Checked;
            SqlParameter pCate = new SqlParameter("NewsCateID", SqlDbType.Int);
            pCate.Value = DropDownList1.SelectedValue;
            if (Request.QueryString["id"] == null)
            {
                
                SqlParameter insertedKey = new SqlParameter("VideoID", SqlDbType.Int);
                insertedKey.Direction = ParameterDirection.Output;
                string NewsID = DataAccessLayer.ExcuteNoneQueryHasOutput("Video_InserVideo", "VideoID", pTitle, pLink,pCate,
                                                                         pHot, insertedKey);
                GetVideo();
                txtTitle.Text = "";
                txtLink.Text = "";
                chkHot.Checked = false;
               
            }
            else
            {
                string id = Request.QueryString["id"] ?? "0";
                SqlParameter videoID = new SqlParameter("VideoID", SqlDbType.Int);
                videoID.Value = id;
                string NewsID = DataAccessLayer.ExcuteNoneQueryHasOutput("Video_UpdateVideo", "VideoID", pTitle, pLink, pCate,
                                                                         pHot, videoID);
                GetVideo();
                Button1.Visible = true;
            }
        }
    }
}