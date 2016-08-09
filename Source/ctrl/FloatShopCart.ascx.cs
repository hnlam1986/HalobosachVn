using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaloBoSach.ctrl
{
    public partial class FloatShopCart : System.Web.UI.UserControl
    {
        public string Items { get; set; }
        public string Total { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblItem.Text = Items;
            lblTotal.Text = Total;
        }
    }
}