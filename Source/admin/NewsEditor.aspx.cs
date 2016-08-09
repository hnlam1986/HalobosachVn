using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class NewsEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "edit")
            {
                divCategory.Visible = true;
                LoadLeafCategory();
                LoadEditNews();
                if(Request.QueryString["reload"]!=null)
                {
                    string cateID = Request.QueryString["cateID"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HaloBoSach",
                           "$(window.opener.document.getElementById('" + cateID + "')).children(\"a\").get(0).click();", true);
                }
            }
            else
            {
                divCategory.Visible = false;
            }
        }
        private void LoadEditNews()
        {
            if (Request.QueryString["id"] != null)
            {
                string id = Request.QueryString["id"];
                SqlParameter paramNewsID = new SqlParameter("NewsID", SqlDbType.Int);
                paramNewsID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByNewID", paramNewsID);
                string NewsTitle = "";
                string NewsContent = "";
                string SourceNews = "";
                string NewsCateID = "";
                string ShortContent = "";
                bool slide = false;
                bool hotnews = false;
                bool mostview = false;
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    NewsTitle = dr["NewsTitle"].ToString();
                    NewsContent = dr["NewsContent"].ToString();
                    SourceNews = dr["SourceNews"].ToString();
                    NewsCateID = dr["NewsCateID"].ToString();
                    ShortContent = dr["ShortContent"].ToString();
                    hdImage.Value = dr["ThumnailImagePath"].ToString();

                    slide = dr["SlideShow"] != null ? bool.Parse(dr["SlideShow"].ToString()) : false;
                    hotnews = dr["HotNews"] != null ? bool.Parse(dr["HotNews"].ToString()) : false;
                    mostview = dr["MostView"] != null ? bool.Parse(dr["MostView"].ToString()) : false;
                }
                txtTitle.Text = NewsTitle;
                txtSource.Text = SourceNews;
                ListItem selectedListItem = ddlCategory.Items.FindByValue(NewsCateID);
                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
                txtShortText.Text = ShortContent;
                hidContent.Value = NewsContent;
                chkSlide.Checked = slide;
                chkHotNews.Checked = hotnews;
                chkMostReview.Checked = mostview;
                string imgUrl = !string.IsNullOrWhiteSpace(hdImage.Value) ? "/NewsAvarta/" + hdImage.Value : "/Images/logo.png";
                imgNews.ImageUrl = imgUrl;
            }
        }
        private void LoadLeafCategory()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetAllLeafNode", null);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    ddlCategory.Items.Add(new ListItem(text, id, true));
                }
            }
            ddlCategory.DataBind();
        }
        private void InsertNews()
        {
            if (Request.QueryString["id"] != null)
            {
                string filename = fuAvarta.FileName;
                string extention = string.IsNullOrEmpty(filename) ? "" : filename.Substring(filename.IndexOf("."));
                string newFileName = string.IsNullOrEmpty(filename) ? "" : Guid.NewGuid().ToString() + extention;
                hdImage.Value = newFileName;
                string cateId = Request.QueryString["id"];
                SqlParameter paramNewsTitle = new SqlParameter("NewsTitle", SqlDbType.NVarChar);
                paramNewsTitle.Value = txtTitle.Text;
                SqlParameter paramThumnailImagePath = new SqlParameter("ThumnailImagePath", SqlDbType.NVarChar);
                paramThumnailImagePath.Value = newFileName;
                SqlParameter paramNewsContent = new SqlParameter("NewsContent", SqlDbType.NVarChar);
                paramNewsContent.Value = hidContent.Value;
                SqlParameter paramSourceNews = new SqlParameter("SourceNews", SqlDbType.NVarChar);
                paramSourceNews.Value = txtSource.Text;
                SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramNewsCateID.Value = cateId;
                SqlParameter paramShortContent = new SqlParameter("ShortContent", SqlDbType.NVarChar);
                paramShortContent.Value = txtShortText.Text;
                SqlParameter paramSlideShow = new SqlParameter("SlideShow", SqlDbType.Bit);
                paramSlideShow.Value = chkSlide.Checked;
                SqlParameter paramHotNews = new SqlParameter("HotNews", SqlDbType.Bit);
                paramHotNews.Value = chkHotNews.Checked;
                SqlParameter paramMostView = new SqlParameter("MostView", SqlDbType.Bit);
                paramMostView.Value = chkMostReview.Checked;
                SqlParameter insertedKey = new SqlParameter("NewsID", SqlDbType.Int);
                insertedKey.Direction = ParameterDirection.Output;
                string NewsID = DataAccessLayer.ExcuteNoneQueryHasOutput("News_InserNews", "NewsID", paramNewsTitle, paramThumnailImagePath,
                    paramNewsContent, paramSourceNews, paramNewsCateID,paramShortContent,paramSlideShow,paramHotNews,paramMostView, insertedKey);
           
                if (!string.IsNullOrEmpty(NewsID))
                {
                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        fuAvarta.SaveAs(Server.MapPath("/NewsAvarta/" + newFileName));
                    }
                    Response.Redirect("NewsEditor.aspx?action=edit&reload=true&id=" + NewsID + "&cateId=" + cateId,
                                      false);
                }
            }
        }
        private void EditNews()
        {
            if (Request.QueryString["id"] != null)
            {
                string filename = fuAvarta.FileName;
                string extention =string.IsNullOrEmpty(filename)?"": filename.Substring(filename.IndexOf("."));
                string newFileName = string.IsNullOrEmpty(filename) ? "" : Guid.NewGuid().ToString() + extention;
                
                string Id = Request.QueryString["id"];
                string cateID = ddlCategory.SelectedValue;
                SqlParameter paramNewsTitle = new SqlParameter("NewsTitle", SqlDbType.NVarChar);
                paramNewsTitle.Value = txtTitle.Text;
                SqlParameter paramThumnailImagePath = new SqlParameter("ThumnailImagePath", SqlDbType.NVarChar);
                paramThumnailImagePath.Value = newFileName;
                SqlParameter paramNewsContent = new SqlParameter("NewsContent", SqlDbType.NVarChar);
                paramNewsContent.Value = hidContent.Value;
                SqlParameter paramSourceNews = new SqlParameter("SourceNews", SqlDbType.NVarChar);
                paramSourceNews.Value = txtSource.Text;
                SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramNewsCateID.Value = cateID;
                SqlParameter paramShortContent = new SqlParameter("ShortContent", SqlDbType.NVarChar);
                paramShortContent.Value = txtShortText.Text;
                SqlParameter insertedKey = new SqlParameter("NewsID", SqlDbType.Int);
                insertedKey.Value = Id;
                SqlParameter paramSlideShow = new SqlParameter("SlideShow", SqlDbType.Bit);
                paramSlideShow.Value = chkSlide.Checked;
                SqlParameter paramHotNews = new SqlParameter("HotNews", SqlDbType.Bit);
                paramHotNews.Value = chkHotNews.Checked;
                SqlParameter paramMostView = new SqlParameter("MostView", SqlDbType.Bit);
                paramMostView.Value = chkMostReview.Checked;
                bool res = DataAccessLayer.ExcuteNoneQuery("News_EditNews", paramNewsTitle, paramThumnailImagePath,
                                                paramNewsContent, paramSourceNews, paramNewsCateID, paramShortContent,
                                                insertedKey, paramSlideShow, paramHotNews, paramMostView);
                if (res)
                {
                    if (!string.IsNullOrEmpty(fuAvarta.FileName))
                    {
                        fuAvarta.SaveAs(Server.MapPath("/NewsAvarta/" + newFileName));
                        if (File.Exists(Server.MapPath("/NewsAvarta/" + hdImage.Value)))
                        {
                            File.Delete(Server.MapPath("/NewsAvarta/" + hdImage.Value));
                        }
                        hdImage.Value = newFileName;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HaloBoSach",
                                                        "$(window.opener.document.getElementById('" + cateID +
                                                        "')).children(\"a\").get(0).click();", true);
                }
            }
        }
        protected void btnSaveTin_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                switch (Request.QueryString["action"])
                {
                    case "new":
                        InsertNews();
                        break;
                    case "edit":
                        EditNews();
                        break;
                }
            }
        }
    }
}