using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamProject
{
    public partial class logon : System.Web.UI.Page
    {
        
            KarateSchoolDataContext dbcon;
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1) (1).mdf\";Integrated Security=True";
            
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateSchoolDataContext(conn);
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string nUserName = Login1.UserName;
            string nPassword = Login1.Password;


            HttpContext.Current.Session["nUserName"] = nUserName;
            HttpContext.Current.Session["UserPassword"] = nPassword;



            // Search for the current User, validate UserName and Password
            NetUser myUser = (from x in dbcon.NetUsers
                                    where x.UserName == HttpContext.Current.Session["nUserName"].ToString()
                                    && x.UserPassword == HttpContext.Current.Session["uPass"].ToString()
                                    select x).First();

            if (myUser != null)
            {
                //Add UserID and User type to the Session
                HttpContext.Current.Session["UserID"] = myUser.UserID;
                HttpContext.Current.Session["UserType"] = myUser.UserType;

            }
            if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "student")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/StudentInfo/studentpage.aspx");
            }
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "advisor")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/AdvisorInfo/advisorpage.aspx");
            }
            else
                Response.Redirect("Logon.aspx", true);

        }
    }
}