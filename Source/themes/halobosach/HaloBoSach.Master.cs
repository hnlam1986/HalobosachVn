using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;
using HaloBoSach.entity;

namespace HaloBoSach
{
    public partial class HaloBoSach : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            ShoppingCart cart = Utilities.GetShopCartFromSession(Session);
            FloatShopCart.Items = cart.TotalItem.ToString();
            FloatShopCart.Total = Utilities.FormatCurrency(cart.TotalPrice);
            LoadSettingHomePage();
        }
        private void LoadSettingHomePage()
        {
            if (Session["HowToUse"] != null && Session["WorkingTime"] != null)
            {
                lnkWorkingTime.HRef = Session["WorkingTime"].ToString();
                lnkHowToUse.HRef = Session["HowToUse"].ToString();
            }
            else
            {
                DataSet ds = DataAccessLayer.ExecuteDataSet("Config_LoadSettingHomePage");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        string name = dataRow["VariableName"].ToString();
                        string value = dataRow["Value"].ToString();
                        switch (name)
                        {
                            case "WorkingTime":
                                lnkWorkingTime.HRef = value;
                                Session["WorkingTime"] = value;
                                break;
                            case "HowToUse":
                                lnkHowToUse.HRef = value;
                                Session["HowToUse"] = value;
                                break;

                        }

                    }
                }
            }
        }
    }
}