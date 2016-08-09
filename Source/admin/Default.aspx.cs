using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            SqlParameter paramUserName = new SqlParameter("UserName", SqlDbType.VarChar);
            paramUserName.Value = txtUserName.Text;
            SqlParameter paramPassword = new SqlParameter("Password", SqlDbType.VarChar);
            paramPassword.Value = txtPassword.Text;
            DataSet ds = DataAccessLayer.ExecuteDataSet("UserLogin_GetUserByUserName", paramUserName, paramPassword);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Session["tgsk_logon"] = true;
                Session["UserName"] = txtUserName.Text;
                Response.Redirect("/admin/NewsCategoryManagement.aspx");
                lblMsg.Visible = false;
            }
            else
            {
                lblMsg.Visible = true;
            }
        }
    }
}