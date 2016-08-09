using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaloBoSach.admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                hdUserName.Value = Session["UserName"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}