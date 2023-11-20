using System;
using System.Web;
using System.Web.Security;

namespace TeamProject
{
    public partial class Instructor1 : System.Web.UI.Page
    {
        KarateSchoolDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1) (1).mdf\";Integrated Security=True";
      
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["nUserName"].ToString().Trim() == "user2")
                {
                    dbcon = new KarateSchoolDataContext(conn);
                    string username = HttpContext.Current.Session["nUserName"].ToString().Trim();

                    var records = from item in dbcon.NetUsers
                                  select item;

                    GridView1.DataSource = records;
                    GridView1.DataBind();
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("logon.aspx", true);
                }
            }
        }
    }
}