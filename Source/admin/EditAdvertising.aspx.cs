using System;
using System.Collections.Generic;
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
    public partial class EditAdvertising : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
             if (Request.QueryString["action"] != null && Request.QueryString["action"] == "edit")
             {
                 LoadAdvertising();
                 string id = Request.QueryString["id"];
                 if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"] == "false")
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "HaloBoSachUpdateAdv",
                            "$(window.opener.document.getElementById('refresh_adv_" + id + "')).get(0).click();", true);
                 }
                 else if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"]=="true")
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "HaloBoSachUpdateAdv",
                            "$(window.opener.document.getElementById('hidNewAdvId')).val('" + id + "');" +
                            "$(window.opener.document.getElementById('advNewRefresh')).get(0).click();", true);
                 }
             }
        }

        protected void btnSubmitTop_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "new")
                {
                    InsertAdvertising();
                }
                else
                {
                    if (Request.QueryString["id"] != null)
                    {
                        UpdateAdvertising();
                    }
                }
                
            }
        }

        private void InsertAdvertising()
        {
            SqlParameter AdvImagePath = new SqlParameter("AdvImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }
            AdvImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter DisplayFrom = new SqlParameter("DisplayFrom", SqlDbType.DateTime);
            DisplayFrom.Value =DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value = DateTime.Parse(dtEnd.Text);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;
            SqlParameter Size = new SqlParameter("IsBigSize", SqlDbType.VarChar);
            Size.Value = chkKichThuoc.Checked;
            SqlParameter Location = new SqlParameter("ShowOnTop", SqlDbType.VarChar);
            Location.Value = chkViTri.Checked;

            SqlParameter Ngang = new SqlParameter("Ngang", SqlDbType.Bit);
            Ngang.Value = false;
            SqlParameter Doc = new SqlParameter("Doc", SqlDbType.Bit);
            Doc.Value = false;
            SqlParameter HaiBen = new SqlParameter("Haiben", SqlDbType.Bit);
            HaiBen.Value = false;
            SqlParameter AnhLon = new SqlParameter("AnhLon", SqlDbType.Bit);
            AnhLon.Value = false;
            SqlParameter AnhNgang = new SqlParameter("AnhNgang", SqlDbType.Bit);
            AnhNgang.Value = false;
            if(rdTop.Checked)
            {
                Ngang.Value = true;
            }else if(rdInside.Checked)
            {
                Doc.Value = true;
            }else if (rdFloat.Checked)
            {
                HaiBen.Value = true;
            }else if(rdAnhNgang.Checked)
            {
                AnhNgang.Value = true;
            }
            else if (rdAnhLon.Checked)
            {
                AnhLon.Value = true;
            }

            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Direction = ParameterDirection.Output;
            string res = DataAccessLayer.ExcuteNoneQueryHasOutput("Adv_InsertAdv", "AdvID", AdvImagePath, DisplayFrom, DisplayTo,
                                                       LinkURL,Size,Location, AdvID,Doc,Ngang,HaiBen,AnhLon,AnhNgang);
            if (!string.IsNullOrEmpty(res))
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    fuLogo.SaveAs(Server.MapPath("/AdvImage/" + guid));
                }
                Response.Redirect("EditAdvertising.aspx?action=edit&reload=true&id=" + res,
                             false);
            }
        }

        private void UpdateAdvertising()
        {
            string id = Request.QueryString["id"];
            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Value = id;
            SqlParameter AdvImagePath = new SqlParameter("AdvImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }
            AdvImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter DisplayFrom = new SqlParameter("DisplayFrom", SqlDbType.DateTime);
            DisplayFrom.Value =DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value =DateTime.Parse( dtEnd.Text);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;
            SqlParameter Size = new SqlParameter("IsBigSize", SqlDbType.VarChar);
            Size.Value = chkKichThuoc.Checked;
            SqlParameter Location = new SqlParameter("ShowOnTop", SqlDbType.VarChar);
            Location.Value = chkViTri.Checked;

            SqlParameter Ngang = new SqlParameter("Ngang", SqlDbType.Bit);
            Ngang.Value = false;
            SqlParameter Doc = new SqlParameter("Doc", SqlDbType.Bit);
            Doc.Value = false;
            SqlParameter HaiBen = new SqlParameter("Haiben", SqlDbType.Bit);
            HaiBen.Value = false;
            SqlParameter AnhLon = new SqlParameter("AnhLon", SqlDbType.Bit);
            AnhLon.Value = false;
            SqlParameter AnhNgang = new SqlParameter("AnhNgang", SqlDbType.Bit);
            AnhNgang.Value = false;
            if (rdTop.Checked)
            {
                Ngang.Value = true;
            }
            else if (rdInside.Checked)
            {
                Doc.Value = true;
            }
            else if (rdFloat.Checked)
            {
                HaiBen.Value = true;
            }
            else if (rdAnhNgang.Checked)
            {
                AnhNgang.Value = true;
            }
            else if (rdAnhLon.Checked)
            {
                AnhLon.Value = true;
            }

            bool res = DataAccessLayer.ExcuteNoneQuery("Adv_UpdateAdv", AdvID, AdvImagePath, DisplayFrom,
                                                       DisplayTo,
                                                       LinkURL, Size, Location,Ngang,Doc,HaiBen,AnhLon,AnhNgang);
            if (res)
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    
                    fuLogo.SaveAs(Server.MapPath("/AdvImage/" + guid));
                    if (File.Exists(Server.MapPath("/AdvImage/" + hdImage.Value)))
                    {
                        File.Delete(Server.MapPath("/AdvImage/" + hdImage.Value));
                    }
                    hdImage.Value = fuLogo.FileName;
                }
                Response.Redirect("EditAdvertising.aspx?action=edit&reload=false&id=" + id,
                             false);
                
            }
        }

        private void LoadAdvertising()
        {
            string id = Request.QueryString["id"];
            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string imgPath = dr["AdvImagePath"].ToString();
                string fromdate = dr["DisplayFrom"].ToString();
                string todate = dr["DisplayTo"].ToString();
                string url = dr["LinkURL"].ToString();
                bool size = (bool)dr["IsBigSize"];
                bool location = (bool)dr["ShowOnTop"];
                bool ngang = (bool)(dr["Horizontial"].ToString() == "" ? false : dr["Horizontial"]);
                bool doc = (bool)(dr["Vertical"].ToString() == "" ? false : dr["Vertical"]);
                bool haiben = (bool)(dr["TwoFloat"].ToString() == "" ? false : dr["TwoFloat"]);
                bool anhlon = (bool)(dr["Anhlon"].ToString() == "" ? false : dr["Anhlon"]);
                bool anhngang = (bool)(dr["AnhNgang"].ToString() == "" ? false : dr["AnhNgang"]);
                hdImage.Value = imgPath;
                dtStart.Text =DateTime.Parse(fromdate).ToString("yyyy/MM/dd");
                dtEnd.Text = DateTime.Parse(todate).ToString("yyyy/MM/dd");
                txtNavigation.Text = url;
                chkKichThuoc.Checked = size;
                chkViTri.Checked = location;
                rdTop.Checked = ngang;
                rdInside.Checked = doc;
                rdFloat.Checked=haiben;
                rdAnhLon.Checked = anhlon;
                rdAnhNgang.Checked = anhngang;
            }
        }
    }
}