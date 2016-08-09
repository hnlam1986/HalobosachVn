using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach
{
    public partial class FAQ : System.Web.UI.Page
    {
        string collapeItem = @"<div class='panel panel-default'>
                                <div class='panel-heading' role='tab' id='heading{2}'>
                                  <h4 class='panel-title'>
                                    <a class='collapsed' role='button' data-toggle='collapse' data-parent='#accordion' href='#collapse{2}' aria-expanded='false' aria-controls='collapse{2}'>
                                      <div class='faq-question'>{0}</div>
                                    </a>
                                  </h4>
                                </div>
                                <div id='collapse{2}' class='panel-collapse collapse' role='tabpanel' aria-labelledby='heading{2}'>
                                  <div class='panel-body'>
                                    <div class='faq-answer-icon'></div>
                                    <div class='faq-answer-content'>{1}</div>
                                  </div>
                                </div>
                              </div>";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Faq_GetAllQuestions");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div class='panel-group' id='accordion' role='tablist' aria-multiselectable='false'>");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string title = row["NewsTitle"].ToString();
                    string content = row["NewsContent"].ToString();
                    string id = row["NewsID"].ToString();
                    string faqItem = string.Format(collapeItem, title, content, id);
                    sb.Append(faqItem);
                }
                sb.Append("</div>");
                faqContent.InnerHtml = sb.ToString();
            }
        }
    }
}