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
    public partial class AlbumManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            GetAlbums();
            GetCategory();
        }
        private void GetAlbums()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadAllAlbum");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
        private void GetCategory()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadAlbumCategory");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList1.Items.Add(new ListItem(text, id, true));
                }
            }
        }
        protected void btnSubmitTop_Click(object sender, EventArgs e)
        {
            SqlParameter paramAlbumTitle = new SqlParameter("AlbumTitle", SqlDbType.NVarChar);
            paramAlbumTitle.Value = txtAlbum.Text;
            SqlParameter paramFolder = new SqlParameter("FolderPath", SqlDbType.NVarChar);
            string FolderName= Guid.NewGuid().ToString();
            paramFolder.Value = FolderName;
            SqlParameter paramHot = new SqlParameter("HotAlbum", SqlDbType.Bit);
            paramHot.Value = chkHot.Checked;
            SqlParameter paramCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
            paramCateID.Value = DropDownList1.SelectedValue;
            SqlParameter insertedKey = new SqlParameter("AlbumID", SqlDbType.Int);
            insertedKey.Direction = ParameterDirection.Output;
            string NewsID = DataAccessLayer.ExcuteNoneQueryHasOutput("Album_InsertAlbum", "AlbumID", paramAlbumTitle, insertedKey, paramFolder, paramHot, paramCateID);
            string fullPath = Server.MapPath("/Albums/" + FolderName);
            Directory.CreateDirectory(fullPath);
            txtAlbum.Text = "";
            GetAlbums();
        }


        
    }
}