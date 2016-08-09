using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.ctrl
{
    public partial class SlideShow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet(StoreProcedureGetData);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                divSlideShow.Visible = false;
            }
        }

        public string StoreProcedureGetData
        {
            get;
            set;
        }
        public string Height
        {
            get;
            set;
        }
        public bool IsIncludeScript { get; set; }
        public bool ShowCaption { get; set; }
        public bool CanBookTicket { get; set; }
        private string _effect = "simpleFade";
        public string Effect { get{return _effect;} set{_effect=value;} }

        public bool CanBooking(string NgayDien, string SuatDien)
        {
            DateTime suatdien = Convert.ToDateTime(NgayDien);
            string[] gioDien = SuatDien.Split(';');
            foreach (string item in gioDien)
            {
                DateTime gio = Convert.ToDateTime(item);
                suatdien = suatdien.AddHours(gio.Hour).AddMinutes(gio.Minute);
                if (suatdien >= DateTime.Now)
                {
                    return true;
                }
            }

            return false;

        }
    }
}