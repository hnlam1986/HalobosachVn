using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using HaloBoSach.Dal;
using HaloBoSach.entity;

namespace HaloBoSach.ctrl
{
    public partial class TopNav : System.Web.UI.UserControl
    {
        public bool BreakLineText { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Session["TopNav"] != null)
            {
                divNav.InnerHtml = Session["TopNav"].ToString();
                return;
            }
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetAllCategoryNode", null);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                List<DynamicMenuItem> lstItemMenu = new List<DynamicMenuItem>();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    int id = int.Parse(dataRow["NewsCateID"].ToString());
                    //string name = BreakLineText ? dataRow["NewsCateName"].ToString().Replace(" ", "<br/>") : dataRow["NewsCateName"].ToString();
                    string name = dataRow["NewsCateName"].ToString();
                    int parent = int.Parse(dataRow["NewsCateParentID"].ToString());
                    string url = dataRow["DirectURL"]!=null?dataRow["DirectURL"].ToString():"";
                    string route = dataRow["RoutePath"] != null ? dataRow["RoutePath"].ToString() : "";
                    int order = int.Parse(dataRow["SortOrder"].ToString() == "" ? "0" : dataRow["SortOrder"].ToString());
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
                StringBuilder sb = new StringBuilder();
                string template_begin = "<ul id=\"menu\">";
                string templateItem = "<li id='nav{2}'><a href=\"{0}\"><div class=\"{3}\"><span>{1}</span></div></a></li>";//
                string templateSubItem = "<li id='nav{2}'><a href=\"{0}\"><div class=\"nav-sub-item\">{1}</div></a></li>";//
                string templateSubItem_start = "<li class=\"sub\" id='nav{0}'>" + //<li class="sub"><a href="category.htm">S?n ph?m</a>
                                               "<a href=\"{3}\"><div class=\"{2}\"><span>{1}</span></div></a>" +
                                               "<ul>";
                string templateSubItem_end = "</ul></li>";
                string template_end = "</ul>";
                sb.Append(template_begin);
                int index = 1;
                
                foreach (DynamicMenuItem item in itemLvl0)
                {
                    string css = "nav-item";
                    if (index == 1)
                    {
                        css = "nav-first-item";
                    }
                    else if (index == itemLvl0.Count)
                    {
                        css = "nav-last-item";
                    }
                    string url = "~/" + Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "");
                    if (item.DirectUrl.Length > 0)
                    {
                        url = "~/" + item.DirectUrl;
                        
                    }
                    if (item.SubItem==null||item.SubItem.Count == 0)//no sub
                    {
                        string compileUrl= Utilities.GetFriendlyUrl(Request, url, item.ParentItemId, item.ItemId);
                        string name = BreakLineText ? item.ItemName.Replace(" ", "<br/>") : item.ItemName;
                        string itemMenu = string.Format(templateItem, compileUrl, name, item.ItemId, css);
                        if (item.DirectUrl == "#")
                        {
                            compileUrl = "#";
                            itemMenu = string.Format(templateItem, compileUrl, item.ItemName, "", css);
                        }
                        sb.Append(itemMenu);
                    }
                    else//has sub
                    {
                        string compileUrl = Utilities.GetFriendlyUrl(Request, url, item.ParentItemId, item.ItemId);
                        string name = BreakLineText ? item.ItemName.Replace(" ", "<br/>") : item.ItemName;
                        string itemSubMenu = string.Format(templateSubItem_start, item.ItemId, name, css, compileUrl);
                        if (item.DirectUrl == "#")
                        {
                            compileUrl = "#";
                            itemSubMenu = string.Format(templateSubItem_start, item.ItemId, name, css, compileUrl);
                        }
                        sb.Append(itemSubMenu);
                        //add sub item
                        foreach (DynamicMenuItem menuItem in item.SubItem)
                        {
                            url = "~/" + Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "") + "-" + Utilities.ConvertUnicodeToAscii(menuItem.ItemName).ToLower().Replace("/", "");
                            //name = BreakLineText ? menuItem.ItemName.Replace(" ", "<br/>") : menuItem.ItemName;
                            string itemMenu = string.Format(templateSubItem, Utilities.GetFriendlyUrl(Request, url, menuItem.ParentItemId, menuItem.ItemId), menuItem.ItemName, menuItem.ItemId, css);
                            if (menuItem.DirectUrl.Length > 0)
                            {
                                url = "~/" + Utilities.ConvertUnicodeToAscii(item.ItemName).ToLower().Replace("/", "") + "-" + Utilities.ConvertUnicodeToAscii(menuItem.ItemName).ToLower().Replace("/", "") +"/"+ menuItem.DirectUrl.ToLower();
                                itemMenu = string.Format(templateSubItem, Utilities.GetFriendlyUrl(Request, url), menuItem.ItemName, menuItem.ItemId, css);
                            }
                            
                            sb.Append(itemMenu);
                        }
                        sb.Append(templateSubItem_end);
                    }
                    index++;
                }
                sb.Append(template_end);
                divNav.InnerHtml = sb.ToString();
                Session["TopNav"] = sb.ToString();

            }
        }
    }
}