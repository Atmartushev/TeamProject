using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace TeamProject
{
    public partial class logon : System.Web.UI.Page
    {
        KarateSchoolDataContext dataContext;
        
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1) (1).mdf\";Integrated Security=True";
            
        protected void Page_Load(object sender, EventArgs e)
        {
            dataContext = new KarateSchoolDataContext(conn);
        }


        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string UserName = Login1.UserName;
            string Password = Login1.Password;

            HttpContext.Current.Session["UserName"] = UserName;
            HttpContext.Current.Session["Pass"] = Password;

            // Search for the current User, validate UserName and Password
            NetUser myUser = (from x in dataContext.NetUsers
                              where x.UserName == HttpContext.Current.Session["UserName"].ToString()
                              && x.UserPassword == HttpContext.Current.Session["Pass"].ToString()
                              select x).FirstOrDefault();


            if (myUser != null)
            {
                // Add UserID and User type to the Session
                HttpContext.Current.Session["userID"] = myUser.UserID;
                HttpContext.Current.Session["userType"] = myUser.UserType;

                // Redirect based on user type
                switch (HttpContext.Current.Session["userType"].ToString().Trim())
                {
                    case "Member":
                        HttpContext.Current.Session["memberFirstName"] = $"{myUser.Member.MemberFirstName}";
                        HttpContext.Current.Session["memberLastName"] = $"{myUser.Member.MemberLastName}";
                        FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);
                        Response.Redirect("~/Memberpage/Memberpage.aspx");
                        break;
                    case "Instructor":
                        HttpContext.Current.Session["instructorFirstName"] = $"{myUser.Instructor.InstructorFirstName}";
                        HttpContext.Current.Session["instructorLastName"] = $"{myUser.Instructor.InstructorLastName}";
                        FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);
                        Response.Redirect("~/Instructorpage/Instructorpage.aspx");
                        break;
                    case "Administrator":
                        FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);
                        Response.Redirect("~/Administrator/Administrator.aspx"); // Replace with the actual page for instructors
                        break;
                }
            }
            else
            {
                Response.Redirect("~/logon.aspx", true);
            }
                

        }

    }
}
