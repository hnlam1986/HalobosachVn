using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using HaloBoSach.Dal;
using HaloBoSach.entity;
using Microsoft.AspNet.FriendlyUrls;

namespace HaloBoSach
{
    public class Global : System.Web.HttpApplication
    {
        void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        private void RegistryRout()
        {
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "tin-tuc/{parent}/{id}/{*queryvalues}", "~/NewsList.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "xem-tin/{id}/{*queryvalues}", "~/NewsDetail.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "chi-tiet-san-pham/{id}/{name}/{*queryvalues}", "~/ProductDetail.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "gio-hang/{*queryvalues}", "~/CartSummary.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "gui-don-hang/{*queryvalues}", "~/CheckOut.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "hoan-tat/{*queryvalues}", "~/ThankYou.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "xac-nhan-don-hang/{guid}/{*queryvalues}", "~/admin/ViewOrderDetail.aspx");
            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), "xem-don-hang/{guid}/{*queryvalues}", "~/CustViewOrderDetail.aspx");
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetAllCategoryNode", null);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                List<DynamicMenuItem> lstItemMenu = new List<DynamicMenuItem>();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    int id = int.Parse(dataRow["NewsCateID"].ToString());
                    string name = dataRow["NewsCateName"].ToString();
                    int parent = int.Parse(dataRow["NewsCateParentID"].ToString());
                    string url = dataRow["DirectURL"] != null ? dataRow["DirectURL"].ToString() : "";
                    int order = int.Parse(dataRow["SortOrder"].ToString() == "" ? "0" : dataRow["SortOrder"].ToString());
                    string route = dataRow["RoutePath"] != null ? dataRow["RoutePath"].ToString() : "";
                    DynamicMenuItem item = new DynamicMenuItem(id, name, parent, order, url, route);
                    lstItemMenu.Add(item);
                }
                List<DynamicMenuItem> itemLvl0 = new List<DynamicMenuItem>();
                itemLvl0 = (from item in lstItemMenu where item.ParentItemId == 0 orderby item.SortOrder select item).ToList();
                if (itemLvl0.Count > 0)
                {
                    foreach (DynamicMenuItem itemMenu in itemLvl0)
                    {
                        List<DynamicMenuItem> itemLvl1 = new List<DynamicMenuItem>();
                        itemLvl1 = (from item in lstItemMenu where item.ParentItemId == itemMenu.ItemId orderby item.SortOrder select item).ToList();
                        if (itemLvl1.Count > 0)
                        {
                            itemMenu.SubItem = itemLvl1;
                        }
                    }
                }
                foreach (DynamicMenuItem item in lstItemMenu)
                {
                    if (item.RoutePath != null && !string.IsNullOrEmpty(item.RoutePath))
                    {
                        RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName) + "/{parent}/{id}/{*queryvalues}", "~/" + item.RoutePath);
                    }
                    else
                    {

                        if (item.DirectUrl != null && item.DirectUrl != "#" && !string.IsNullOrEmpty(item.DirectUrl))
                        {
                            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName) + "/{id}/{name}/{*queryvalues}", "~/NewsDetail.aspx");
                        }
                        else
                        {
                            RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName) + "/{parent}/{id}/{*queryvalues}", "~/NewsList.aspx");
                        }
                    }
                    if (item.SubItem != null && item.SubItem.Count > 0)
                    {
                        foreach (DynamicMenuItem sub_item in item.SubItem)
                        {
                            if (sub_item.RoutePath != null && !string.IsNullOrEmpty(sub_item.RoutePath))
                            {
                                RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "") + "-" + Utilities.ConvertUnicodeToAscii(sub_item.ItemName).ToLower().Replace("/", "") + "/{parent}/{id}/{*queryvalues}", "~/" + sub_item.RoutePath);
                            }
                            else
                            {
                                if (sub_item.DirectUrl != null && sub_item.DirectUrl != "#" && !string.IsNullOrEmpty(sub_item.DirectUrl))
                                {
                                    RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "") + "-" + Utilities.ConvertUnicodeToAscii(sub_item.ItemName).ToLower().Replace("/", "") + "/{id}/{name}/{*queryvalues}", "~/NewsDetail.aspx");
                                }
                                else
                                {
                                    RouteTable.Routes.MapPageRoute(Guid.NewGuid().ToString(), Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "") + "-" + Utilities.ConvertUnicodeToAscii(sub_item.ItemName).ToLower().Replace("/", "") + "/{parent}/{id}/{*queryvalues}", "~/NewsList.aspx");
                                }
                            }
                        }
                    }

                }
            }
        }
        void Application_Start(object sender, EventArgs e)
        {
            RegistryRout();
            RouteTable.Routes.EnableFriendlyUrls();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
