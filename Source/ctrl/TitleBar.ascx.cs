using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaloBoSach.ctrl
{
    public partial class TitleBar : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.InnerHtml = Title;
        }
    }
}