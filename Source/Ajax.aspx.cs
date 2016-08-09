using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;
using System.IO;
using HaloBoSach.entity;
using Newtonsoft.Json;

namespace HaloBoSach
{
    public partial class Ajax : System.Web.UI.Page
    {
        private void GetNode()
        {
            if (Request.QueryString["id"] != null)
            {
                string parentNodeId = Request.QueryString["id"];
                bool isHaveData = false;
                StringBuilder html = new StringBuilder();
                int nodeId = 0;
                if (parentNodeId != "#")
                {
                    //load root nodes
                    nodeId = int.Parse(parentNodeId);
                }
                SqlParameter paramParentNodeID = new SqlParameter("ParentNodeID", SqlDbType.Int);
                paramParentNodeID.Value = nodeId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetCategoryByParentNodeID", paramParentNodeID);

                html = new StringBuilder();

                html.AppendLine("<ul>");
                if (parentNodeId == "#")
                {
                    html.AppendLine("<li id=\"0\">Trang chủ<ul>");
                }
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        string id = dataRow["NewsCateID"].ToString();
                        string name = dataRow["NewsCateName"].ToString();
                        string hasChild = dataRow["HasChild"].ToString();
                        string isVisible = dataRow["IsVisible"].ToString();
                        string isCate = dataRow["IsProductCategory"].ToString();
                        if (hasChild == "True")
                        {
                            html.AppendLine(string.Format("<li id=\"{0}\" class=\"jstree-closed\" data-visible=\"{2}\" data-is-category=\"{3}\">{1}</li>", id,
                                                          name, isVisible, isCate));
                        }
                        else
                        {
                            html.AppendLine(string.Format("<li id=\"{0}\"  data-visible=\"{2}\" data-is-category=\"{3}\">{1}</li>", id, name, isVisible, isCate));
                        }
                    }
                }
                if (parentNodeId == "#")
                {
                    html.AppendLine("</ul></li>");
                }
                html.AppendLine("</ul>");


                Response.Write(html.ToString());

            }
        }
        private void Rename()
        {
            if (Request.Form["id"] != null && Request.Form["name"] != null)
            {
                try
                {
                    string id = Request.Form["id"];
                    string name = Request.Form["name"];
                    SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                    paramNewsCateID.Value = id;
                    SqlParameter paramNewsCateName = new SqlParameter("NewsCateName", SqlDbType.NVarChar);
                    paramNewsCateName.Value = name;
                    bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateCategoryName", paramNewsCateID, paramNewsCateName);
                    Response.Write(res);
                }
                catch (Exception ex)
                {
                    Response.Write(false);
                }
            }
        }
        private void Delete()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramNewsCateID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Category_DeleteCategory", paramNewsCateID);
                Response.Write(res);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }

        private void DeleteNews()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("NewsID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("News_DeleteNews", paramNewsID);
                if (res)
                {
                    if (File.Exists(Server.MapPath("/NewsAvarta/" + image)))
                    {
                        File.Delete(Server.MapPath("/NewsAvarta/" + image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }

        private void DeleteProduct()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("ProductID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Product_Delete", paramNewsID);
                if (res)
                {
                    if (File.Exists(Server.MapPath("/Product/" + image)))
                    {
                        File.Delete(Server.MapPath("/Product/" + image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }

        private void Create()
        {
            try
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                string isleaf = Request.Form["leaf"];
                SqlParameter paramNewsCateName = new SqlParameter("NewsCateName", SqlDbType.NVarChar);
                paramNewsCateName.Value = name;
                SqlParameter paramNewsCateParentID = new SqlParameter("NewsCateParentID", SqlDbType.Int);
                paramNewsCateParentID.Value = id;
                SqlParameter paramIsLeaf = new SqlParameter("IsLeaf", SqlDbType.Bit);
                paramIsLeaf.Value = isleaf;
                bool res = DataAccessLayer.ExcuteNoneQuery("Category_CreateNewsCategory", paramNewsCateName, paramNewsCateParentID, paramIsLeaf);
                Response.Write(res);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void GetNewsList()
        {
            if (Request.QueryString["id"] != null)
            {
                string cateId = Request.QueryString["id"];
                string isLeaf = Request.QueryString["isLeaf"];
                SqlParameter paramParentNodeID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramParentNodeID.Value = cateId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByCategoryId", paramParentNodeID);
                StringBuilder html = new StringBuilder();
                string start_template = "<table id=\"tblNews\" class=\"custom-table\">" +
                                        "<thead><tr>" +
                                        "<td>Tiêu Đề</td>" +
                                        "<td>Ngày Đăng</td>" +
                                        "<td>" + (isLeaf == "true" ?
                                        ("<button class=\"btn btn-success btn-sm\"  type=\"button\"" +
                                        "onclick=\"var popup = window.open('NewsEditor.aspx?action=new&id={0}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Đăng thêm tin tức</button>") : "") +
                                        "</td>" +
                                        "</tr></thead>";

                string item_template = "<tr id='news_{2}'>" +
                                       "<td>{0}</td>" +
                                       "<td>{1}</td>" +
                                       "<td>" +
                                       "<button class=\"btn btn-warning btn-sm\"  type=\"button\" onclick=\"var popup = window.open('NewsEditor.aspx?action=edit&id={2}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\">Sửa</button>" +
                                       "<button class=\"btn btn-danger btn-sm\"  type=\"button\" style=\"margin-left:2px;\" " +
                                       "onclick=\"NewsCategoryManagementEvent.DeleteNewsItem({2},'{3}')\"" +
                                       ">Xóa</button>" +
                                       "<button class=\"btn btn-success btn-sm\"  type=\"button\" style=\"margin-left:2px;\" " +
                                       "onclick=\"NewsCategoryManagementEvent.UpdateDirectUrl({4},'{5}')\"" +
                                       ">Chuyển</button>" +
                                       "<img src='img/throbber.gif' id='news_indicator_{2}' style='display:none'/>" +
                                       "</td>" +
                                       "</tr>";

                string end_template = "<tfoot>" +
                                      "<tr>" +
                                      "<td></td>" +
                                      "<td></td>" +
                                      "<td>" + (isLeaf == "true" ?
                                      ("<button class=\"btn btn-success btn-sm\"  type=\"button\" " +
                                      "onclick=\"var popup = window.open('NewsEditor.aspx?action=new&id={0}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Đăng thêm tin tức</button>") : "") +
                                      "</td>" +
                                      "</tr></tfoot></table>";

                StringBuilder sb = new StringBuilder();
                start_template = string.Format(start_template, cateId);
                end_template = string.Format(end_template, cateId);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        string id = dataRow["NewsID"].ToString();
                        string parentDirectUrl = dataRow["ParentDirectUrl"].ToString();
                        string title = dataRow["NewsTitle"].ToString();
                        string posted_date = dataRow["PostedDate"].ToString();
                        string newsImage = dataRow["ThumnailImagePath"].ToString();
                        string formatTitle = string.Format("<a href='/xem-tin/{1}/{0}'>{2}<a>", Utilities.ConvertUnicodeToAscii(title)+".halobosach.vn", id, title, parentDirectUrl);
                        string url = string.Format("{0}/{1}", id, Utilities.ConvertUnicodeToAscii(title) + ".halobosach.vn");
                        sb.AppendLine(string.Format(item_template, formatTitle, posted_date, id, newsImage, cateId, url));
                    }
                }
                string result = start_template + sb.ToString() + end_template;
                Response.Write(result);
            }
        }
        private void UpdateOrder()
        {
            try
            {
                string selected = Request.Form["selected"];
                string children = Request.Form["children"];
                string is_leaf = Request.Form["is_leaf"];
                string parent_node = Request.Form["parent_node"];
                string sort_order = Request.Form["order"];
                if (string.IsNullOrEmpty(children))
                {
                    children = selected;
                }
                string[] order = children.Split(',');
                List<string> list = null;
                if (!order.Contains(selected))
                {
                    children = selected + "," + children;
                    order = children.Split(',');
                    list = order.ToList<string>();
                }
                else
                {
                    list = order.ToList<string>();
                    int old_index = list.IndexOf(selected);
                    list.RemoveAt(old_index);
                    int index_at = int.Parse(sort_order);
                    if (old_index < index_at)
                        index_at -= 1;
                    else if (index_at != 0 && index_at > list.Count - 1)
                        index_at = list.Count - 1;
                    list.Insert(index_at, selected);

                }
                int index = 0;
                foreach (string s in list)
                {
                    SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                    paramNewsCateID.Value = s;
                    SqlParameter paramNewsCateParentID = new SqlParameter("NewsCateParentID", SqlDbType.Int);
                    paramNewsCateParentID.Value = parent_node;
                    SqlParameter paramSortOrder = new SqlParameter("SortOrder", SqlDbType.Int);
                    paramSortOrder.Value = index;
                    SqlParameter paramIsLeaf = new SqlParameter("IsLeaf", SqlDbType.Bit);
                    paramIsLeaf.Value = is_leaf;
                    bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateOrder", paramNewsCateID,
                                                               paramNewsCateParentID, paramSortOrder, paramIsLeaf);
                    index++;
                }
                Response.Write(true);
            }
            catch (Exception)
            {
                Response.Write(false);
            }
        }
        private void DeleteAdv()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("AdvID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Adv_DeleteAdv", paramNewsID);
                if (res)
                {
                    if (File.Exists(Server.MapPath(image)))
                    {
                        File.Delete(Server.MapPath(image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void RefreshAdv()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem(true);
                    string s = string.Format(template, imgPath, fromdate, todate, url, id);
                    Response.Write(s);
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void AddNewAdv()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem();
                    string s = string.Format(template, imgPath, fromdate, todate, url, id);
                    Response.Write(s);
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void DeleteAlbum()
        {
            try
            {
                string id = Request.Form["id"];
                string folder = Request.Form["folder"];
                SqlParameter ID = new SqlParameter("ID", SqlDbType.Int);
                ID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Album_DeleteAlbum", ID);
                if (res)
                {
                    string path = Server.MapPath("/Albums/" + folder);
                    if (Directory.Exists(path))
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                        Directory.Delete(path);
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void DeleteVideo()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter ID = new SqlParameter("ID", SqlDbType.Int);
                ID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Video_DeleteVideo", ID);
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void ChangeDisplayMenuStatus()
        {
            string id = Request.QueryString["id"];
            SqlParameter pID = new SqlParameter("ID", SqlDbType.Int);
            pID.Value = id;
            bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateDisplayStatus", pID);
        }
        private void GetImageFromFolder()
        {
            string path = Request.Form["path"];
            string[] files = Directory.GetFiles(path);
            StringBuilder sb = new StringBuilder();
            foreach (string file in files)
            {
                string imgUrl = file.Replace("\\", "/");
                int index = imgUrl.IndexOf("UserFiles");
                imgUrl = imgUrl.Substring(index);
                imgUrl = "/" + imgUrl;
                sb.Append("<img src=\"" + imgUrl + "\" style=\"width: 100px;float:left;margin-right:10px;margin-bottom:10px\" onclick=\"apply_img('" + imgUrl + "')\"/>");
            }
            Response.Write(sb.ToString());
        }
        private void ChangeCategoryStatus()
        {
            string id = Request.QueryString["id"];
            SqlParameter pID = new SqlParameter("ID", SqlDbType.Int);
            pID.Value = id;
            bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateIsProductCategory", pID);
        }
        private void GetProductList()
        {
            if (Request.QueryString["id"] != null)
            {
                string cateId = Request.QueryString["id"];
                string isLeaf = Request.QueryString["isLeaf"];
                SqlParameter paramParentNodeID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramParentNodeID.Value = cateId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Product_GetProductByCategoryId", paramParentNodeID);
                StringBuilder html = new StringBuilder();
                string start_product_template = "<table id=\"tblNews\" class=\"custom-table\" {1}>" +
                                        "<thead><tr>" +
                                        "<td>Ảnh SP</td>" +
                                        "<td>Tên SP</td>" +
                                        "<td>Giá SP</td>" +
                                        "<td>" + (isLeaf == "true" ?
                                        ("<button class=\"btn btn-success btn-sm\"  type=\"button\"" +
                                        "onclick=\"var popup = window.open('ProductEditor.aspx?action=new&id={0}','EditProductWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Đăng thêm SP</button>") : "") +
                                        "</td>" +
                                        "</tr></thead>";

                string item_product_template = "<tr id='news_{2}'>" +
                                       "<td><img src='{3}' class='img-product'></img></td>" +
                                       "<td>{0}</td>" +
                                       "<td>{1}</td>" +
                                       "<td>" +
                                       "<button class=\"btn btn-warning btn-sm\"  type=\"button\" onclick=\"var popup = window.open('ProductEditor.aspx?action=edit&id={2}','EditProductWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\">Edit</button>" +
                                       "<button class=\"btn btn-danger btn-sm\"  type=\"button\" style=\"margin-left:2px;\" " +
                                       "onclick=\"NewsCategoryManagementEvent.DeleteProductItem({2},'{3}')\"" +
                                       ">Del</button>" +
                                       "<img src='img/throbber.gif' id='news_indicator_{2}' style='display:none'/>" +
                                       "</td>" +
                                       "</tr>";

                string end_product_template = "<tfoot>" +
                                      "<tr>" +
                                      "<td></td>" +
                                      "<td></td>" +
                                      "<td></td>" +
                                      "<td>" + (isLeaf == "true" ?
                                      ("<button class=\"btn btn-success btn-sm\"  type=\"button\" " +
                                      "onclick=\"var popup = window.open('ProductEditor.aspx?action=new&id={0}','EditProductWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Đăng thêm SP</button>") : "") +
                                      "</td>" +
                                      "</tr></tfoot></table>";
                StringBuilder sb = new StringBuilder();
                string start = "";
                string end = string.Format(end_product_template, cateId);
                int index = 0;
                int page_index = 0;
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        if (index == 0)
                        {
                            page_index++;
                            if (page_index > 1)
                            {
                                start = string.Format(start_product_template, cateId, "data-paging='" + page_index + "' style='display:none'");
                            }
                            else
                            {
                                start = string.Format(start_product_template, cateId, "data-paging='" + page_index + "'");
                            }
                            sb.Append(start);
                        }
                        string id = dataRow["ProductId"].ToString();
                        string title = dataRow["ProductName"].ToString();
                        string price = dataRow["Price"].ToString();
                        string image = dataRow["Image"].ToString();
                        string imgUrl = !string.IsNullOrWhiteSpace(image) ? "/product/" + image : "/Images/logo.png";
                        sb.AppendLine(string.Format(item_product_template, title, price, id, imgUrl));
                        index++;
                        if (index == 10)
                        {
                            sb.Append(end);
                            index = 0;
                        }
                    }
                }
                if (index < 10 && index > 0)
                {
                    sb.Append(end);
                    index = 0;
                }
                string pgNews = "<div class='divPgNews'>" + Utilities.BuildPaging("pgNews", "NewsCategoryManagementEvent.SelectedPage", page_index) + "</div>";
                sb.Append(pgNews);
                Response.Write(sb.ToString());
            }
        }
        private void ApplyDirectUrl()
        {
            string id = Request.Form["id"];
            string url = Request.Form["url"];
            SqlParameter ID = new SqlParameter("NewsCateID", SqlDbType.Int);
            ID.Value = id;
            SqlParameter Url = new SqlParameter("DirectURL", SqlDbType.VarChar);
            Url.Value = url;
            bool res = DataAccessLayer.ExcuteNoneQuery("Cate_SaveOnlyDirectLinkByCateId", ID, Url);
            Response.Write(res);
        }
        private void AddToCart()
        {
            string ProductId = Request.Form["ProductId"];
            string ProductName = Request.Form["ProductName"];
            string Price = Request.Form["Price"];
            string Amount = Request.Form["Amount"];
            string ImagePath = Request.Form["ImagePath"];

            CartItem item = new CartItem();
            item.ProductId = int.Parse(ProductId);
            item.ProductName = ProductName;
            item.ImagePath = ImagePath;
            item.Amount = Convert.ToInt32(Amount);
            item.Price = Convert.ToDouble(Price);

            ShoppingCart cart = AddItemToSession(item);
            ReturnCartSummary summary = new ReturnCartSummary(cart);
            string json = JsonConvert.SerializeObject(summary);
            Response.Write(json);
        }
        private ShoppingCart AddItemToSession(CartItem item)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (Session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)Session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            bool exist = false;
            foreach (CartItem i in cart.ListProduct)
            {
                if (i.ProductId == item.ProductId)
                {
                    exist = true;
                    i.Amount += item.Amount;
                    break;
                }
            }
            if (exist == false)
            {
                cart.ListProduct.Add(item);
            }
            Session["ShoppingCart"] = cart;
            return cart;
        }
        private void UpdateCartItem()
        {
            int ProductId = int.Parse(Request.Form["id"].ToString());
            int Quantity = int.Parse(Request.Form["quantity"].ToString());
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (Session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)Session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            double updatedPrice = 0;
            foreach (CartItem i in cart.ListProduct)
            {
                if (i.ProductId == ProductId)
                {
                    i.Amount = Quantity;
                    updatedPrice = i.Total;
                    break;
                }
            }
            Session["ShoppingCart"] = cart;
            ReturnCartSummary summary = new ReturnCartSummary(cart);
            summary.UpdatedTotalPrice =Utilities.FormatCurrency( updatedPrice);
            string json = JsonConvert.SerializeObject(summary);
            Response.Write(json);
        }

        private void ViewOrderUpdateCartItem()
        {
            int CartDetailId = int.Parse(Request.Form["id"].ToString());
            int Quantity = int.Parse(Request.Form["quantity"].ToString());
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (Session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)Session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            double updatedPrice = 0;
            foreach (CartItem i in cart.ListProduct)
            {
                if (i.CartDetailId == CartDetailId)
                {
                    i.Amount = Quantity;
                    updatedPrice = i.Total;
                    break;
                }
            }
            //update DB
            SqlParameter ID = new SqlParameter("CartDetailId", SqlDbType.Int);
            ID.Value = CartDetailId;
            SqlParameter amount = new SqlParameter("Amount", SqlDbType.Int);
            amount.Value = Quantity;
            bool res = DataAccessLayer.ExcuteNoneQuery("ShopCartDetail_UpdateAmount", ID, amount);
            Session["ShoppingCart"] = cart;
            ReturnCartSummary summary = new ReturnCartSummary(cart);
            summary.UpdatedTotalPrice = Utilities.FormatCurrency(updatedPrice);
            string json = JsonConvert.SerializeObject(summary);
            Response.Write(json);
        }

        private void DeteleCartItem()
        {
            int ProductId = int.Parse(Request.Form["id"].ToString());
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (Session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)Session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            double updatedPrice = 0;
            for (int i = 0; i < cart.ListProduct.Count; i++)
            {
                if (cart.ListProduct[i].ProductId == ProductId)
                {
                    cart.ListProduct.RemoveAt(i);
                    break;
                }
            }
            Session["ShoppingCart"] = cart;
            ReturnCartSummary summary = new ReturnCartSummary(cart);
            summary.UpdatedTotalPrice =Utilities.FormatCurrency( updatedPrice);
            string json = JsonConvert.SerializeObject(summary);
            Response.Write(json);
        }
        private void ViewOrderDeteleCartItem()
        {
            int CartDetailId = int.Parse(Request.Form["id"].ToString());
            ShoppingCart cart = new ShoppingCart();
            cart.ListProduct = new List<CartItem>();
            if (Session["ShoppingCart"] != null)
            {
                cart = (ShoppingCart)Session["ShoppingCart"];
                if (cart.ListProduct == null)
                {
                    cart.ListProduct = new List<CartItem>();
                }
            }
            double updatedPrice = 0;
            for (int i = 0; i < cart.ListProduct.Count; i++)
            {
                if (cart.ListProduct[i].CartDetailId == CartDetailId)
                {
                    cart.ListProduct.RemoveAt(i);
                    break;
                }
            }
            //update DB
            SqlParameter ID = new SqlParameter("CartDetailId", SqlDbType.Int);
            ID.Value = CartDetailId;
            bool res = DataAccessLayer.ExcuteNoneQuery("ShopCartDetail_DeleteById", ID);
            Session["ShoppingCart"] = cart;
            ReturnCartSummary summary = new ReturnCartSummary(cart);
            summary.UpdatedTotalPrice = Utilities.FormatCurrency(updatedPrice);
            string json = JsonConvert.SerializeObject(summary);
            Response.Write(json);
        }
        private const string orderEmailTemplate = @"<span>THÔNG TIN XÁC NHẬN ĐƠN HÀNG</span><br />
                                                    <p>Xin chào khách hàng {0},
                                                    <br />
                                                    Công ty HaloFreshBeef xin chân thành cảm ơn quý khách đã tín nhiệm và sử dụng dịch vụ của chúng tôi.<br />
                                                    Chúng tôi vừa nhận được 1 đơn hàng từ quý khách vào lúc: {1}<br />
                                                    Với tổng số món hàng: {2}<br />
                                                    Đơn hàng của quý khách có trị giá: {3}<br /><br />

                                                    Chúng tôi sẽ liên hệ với quý khách để xác nhận đơn hàng trong thời gian sớm nhất.<br /><br />
                                                    <a href='http://{5}/xem-don-hang/{4}'>XEM LẠI ĐƠN HÀNG</a><br />
                                                    Xin cảm ơn.</p>";
        private void SendShopCartInfo()
        {
            bool res = false;
            string CustomerName = Request.Form["CustomerName"].ToString();
            string ShipAddress = Request.Form["ShipAddress"].ToString();
            string Phone = Request.Form["Phone"].ToString();
            string Email = Request.Form["Email"].ToString();
            string Note = Request.Form["Note"].ToString();
            //Insert ShopCart
            SqlParameter paramCustomerName = new SqlParameter("CustomerName", SqlDbType.NVarChar);
            paramCustomerName.Value = CustomerName;
            SqlParameter paramShipAddress = new SqlParameter("ShipAddress", SqlDbType.NVarChar);
            paramShipAddress.Value = ShipAddress;
            SqlParameter paramPhone = new SqlParameter("Phone", SqlDbType.VarChar);
            paramPhone.Value = Phone;
            SqlParameter paramEmail = new SqlParameter("Email", SqlDbType.VarChar);
            paramEmail.Value = Email;
            SqlParameter paramNote = new SqlParameter("Note", SqlDbType.NVarChar);
            paramNote.Value = Note;
            SqlParameter paramGuidId = new SqlParameter("GuidId", SqlDbType.VarChar);
            string cartGuid = Guid.NewGuid().ToString();;
            paramGuidId.Value = cartGuid;
            SqlParameter insertedKey = new SqlParameter("ShopCartId", SqlDbType.Int);
            insertedKey.Direction = ParameterDirection.Output;
            string Id = DataAccessLayer.ExcuteNoneQueryHasOutput("ShopCart_Insert", "ShopCartId", paramCustomerName, paramShipAddress, paramPhone, paramEmail, paramNote, insertedKey, paramGuidId);
            ShoppingCart cart = new ShoppingCart();
            if (Id != "")
            {
                cart = Utilities.GetShopCartFromSession(Session);
                foreach (CartItem item in cart.ListProduct)
                {
                    SqlParameter paramProductId = new SqlParameter("ProductId", SqlDbType.Int);
                    paramProductId.Value = item.ProductId;
                    SqlParameter paramShopCartId = new SqlParameter("ShopCartId", SqlDbType.Int);
                    paramShopCartId.Value = Id;
                    SqlParameter paramProductName = new SqlParameter("ProductName", SqlDbType.NVarChar);
                    paramProductName.Value = item.ProductName;
                    SqlParameter paramPrice = new SqlParameter("Price", SqlDbType.Float);
                    paramPrice.Value = item.Price;
                    SqlParameter paramAmount = new SqlParameter("Amount", SqlDbType.Int);
                    paramAmount.Value = item.Amount;
                    res = DataAccessLayer.ExcuteNoneQuery("ShopCartDetail_Insert", paramProductId, paramShopCartId, paramProductName, paramPrice, paramAmount);
                    if (res == false)
                    {
                        SqlParameter paramShopCartId2 = new SqlParameter("ShopCartId", SqlDbType.Int);
                        paramShopCartId2.Value = Id;
                        DataAccessLayer.ExcuteNoneQuery("ShopCart_Delete", paramShopCartId2);
                        break;
                    }
                }
                
            }
            if (res == true)
            {
                Session["ShoppingCart"] = null;
                string subject = string.Format("[HaloBoSach] - Thông Tin Đặt Hàng - KH:{0}",CustomerName);
                string websiteUrl = ConfigurationManager.AppSettings["websiteDomain"];
                string emailContent = string.Format(orderEmailTemplate, CustomerName, DateTime.Now.ToString("dd/MM/yyyy hh:mm/ss"), cart.TotalItem, Utilities.FormatCurrency( cart.TotalPrice), cartGuid, websiteUrl);
                Utilities.SendEmail(Utilities.SendTypeEmail.Order ,Email, subject, emailContent);
            }
            Response.Write(res);
        }
        private void ProcessedOrder()
        {
            string Id = Request.Form["ShopCartId"].ToString();
            SqlParameter paramShopCartId = new SqlParameter("ShopCartId", SqlDbType.Int);
            paramShopCartId.Value = Id;
            bool res = DataAccessLayer.ExcuteNoneQuery("ShopCart_OrderProcessed", paramShopCartId);
            Session["ShoppingCart"] = null;
            Response.Write(res);
        }
        private void UpdateCustomerInfo()
        {
            bool res = false;
            string CustomerName = Request.Form["CustomerName"].ToString();
            string ShipAddress = Request.Form["ShipAddress"].ToString();
            string Phone = Request.Form["Phone"].ToString();
            string Email = Request.Form["Email"].ToString();
            string Note = Request.Form["Note"].ToString();
            string ShopCartId = Request.Form["ShopCartId"].ToString();
            //Insert ShopCart
            SqlParameter paramCustomerName = new SqlParameter("CustomerName", SqlDbType.NVarChar);
            paramCustomerName.Value = CustomerName;
            SqlParameter paramShipAddress = new SqlParameter("ShipAddress", SqlDbType.NVarChar);
            paramShipAddress.Value = ShipAddress;
            SqlParameter paramPhone = new SqlParameter("Phone", SqlDbType.VarChar);
            paramPhone.Value = Phone;
            SqlParameter paramEmail = new SqlParameter("Email", SqlDbType.VarChar);
            paramEmail.Value = Email;
            SqlParameter paramNote = new SqlParameter("Note", SqlDbType.NVarChar);
            paramNote.Value = Note;
            SqlParameter paramShopCartId = new SqlParameter("ShopCartId", SqlDbType.Int);
            paramShopCartId.Value = ShopCartId;
            res = DataAccessLayer.ExcuteNoneQuery("ShopCart_Update", paramCustomerName, paramShipAddress, paramPhone, paramEmail, paramNote, paramShopCartId);
            Response.Write(res);
        }
        private void SendRequestEmail()
        {
            bool res = false;
            try
            {
                string CustomerName = Request.Form["CustomerName"].ToString();
                string Phone = Request.Form["Phone"].ToString();
                string Email = Request.Form["Email"].ToString();
                string RequestContent = Request.Form["Request"].ToString();
                string subjectTemplate = "[HaloBoSach] - Thông tin liên hệ - KH:{0}";
                string content = @"<div>
                                    <h2>THÔNG TIN LIỆN HỆ</h2><br />
                                    <span>Họ tên: {0}</span><br />
                                    <span>Số điện thoại: {1}</span><br />
                                    <span>Email: {2}</span><br />
                                    <h2>NỘI DUNG LIỆN HỆ</h2><br />
                                    <span>{3}</span>
                                </div>";
                Utilities.SendEmail(Utilities.SendTypeEmail.Contact, "", string.Format(subjectTemplate, CustomerName), string.Format(content, CustomerName, Phone, Email, RequestContent));
                res = true;
            }
            catch (Exception ex)
            {
            }
            Response.Write(res);
        }
        private void UpdatePassword()
        {
            bool res = false;
            string password = Request.Form["Password"].ToString();
            string username = Request.Form["UserName"].ToString();
            SqlParameter paramUserName = new SqlParameter("UserName", SqlDbType.VarChar);
            paramUserName.Value = username;
            SqlParameter paramPassword = new SqlParameter("Password", SqlDbType.VarChar);
            paramPassword.Value = password;
            res = DataAccessLayer.ExcuteNoneQuery("UserLogin_UpdatePassword", paramUserName, paramPassword);
            Response.Write(res);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                switch (Request.QueryString["action"])
                {
                    case "get":
                        GetNode();
                        break;
                    case "rename":
                        Rename();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "create":
                        Create();
                        break;
                    case "getnewslist":
                        GetNewsList();
                        break;
                    case "deletenews":
                        DeleteNews();
                        break;
                    case "order":
                        UpdateOrder();
                        break;
                    case "deleteadv":
                        DeleteAdv();
                        break;
                    case "refreshadv":
                        RefreshAdv();
                        break;
                    case "addnewadv":
                        AddNewAdv();
                        break;
                    case "deletealbum":
                        DeleteAlbum();
                        break;
                    case "deletevideo":
                        DeleteVideo();
                        break;
                    case "changestatus":
                        ChangeDisplayMenuStatus();
                        break;
                    case "getimage":
                        GetImageFromFolder();
                        break;
                    case "changecategorystatus":
                        ChangeCategoryStatus();
                        break;
                    case "getproductlist":
                        GetProductList();
                        break;
                    case "deleteproduct":
                        DeleteProduct();
                        break;
                    case "applydirect":
                        ApplyDirectUrl();
                        break;
                    case "addtocart":
                        AddToCart();
                        break;
                    case "updatecartitem":
                        UpdateCartItem();
                        break;
                    case "deletecartitem":
                        DeteleCartItem();
                        break;
                    case "sendshopcart":
                        SendShopCartInfo();
                        break;
                    case "processedorder":
                        ProcessedOrder();
                        break;
                    case "vieworderupdatecartitem":
                        ViewOrderUpdateCartItem();
                        break;
                    case "vieworderdeletecartitem":
                        ViewOrderDeteleCartItem();
                        break;
                    case "updatecustomerinfo":
                        UpdateCustomerInfo();
                        break;
                    case "sendrequestemail":
                        SendRequestEmail();
                        break;
                    case "changepassword":
                        UpdatePassword();
                        break;
                }
            }
        }
    }
}