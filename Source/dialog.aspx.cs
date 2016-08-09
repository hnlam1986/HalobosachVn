using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaloBoSach
{
    public partial class dialog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litDirectory.Text = LoadDirectory(Server.MapPath("/UserFiles"),false,1,0);
        }
        private string LoadDirectory(string directory,bool continued,int parentsize,int ind)
        {
            DirectoryInfo di=new DirectoryInfo(directory);

            StringBuilder sb=new StringBuilder();
            if (!continued)
            {
                sb.Append("<ul>");
            }
            sb.Append(string.Format("<li data-path=\"{1}\" >{0}", di.Name, directory));
            //sb.Append(string.Format("<li >{0}", di.Name));
            string[] dirs = Directory.GetDirectories(directory);
            int index = 0;
            foreach (var dir in dirs)
            {
                if (index==0)
                {
                    continued = false;
                }
                else
                {
                    continued = true;
                }
                sb.Append(LoadDirectory(dir, continued, dirs.Count(),index));
                index++;
            }
            //if (index>0)
            //{
            //    sb.Append("</ul>"); 
            //}
            sb.Append("</li>");
            if (parentsize==ind+1)
            {
                sb.Append("</ul>");
            }
            
            return sb.ToString();
        }
        protected void btnNewFolder_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}