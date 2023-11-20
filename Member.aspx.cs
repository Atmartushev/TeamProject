using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamProject
{
    public partial class Member1 : System.Web.UI.Page
    {
        KarateSchoolDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1) (1).mdf\";Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() != "member")
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("logon.aspx", true);
                }
                else
                {
                    dbcon = new KarateSchoolDataContext(conn);
                    string userID = HttpContext.Current.Session["userID"].ToString().Trim();

                    var records = from item in dbcon.NetUsers
                                  select item;

                    GridView1.DataSource = records;
                    GridView1.DataBind();

                    
                }
            }
            }
    }
}