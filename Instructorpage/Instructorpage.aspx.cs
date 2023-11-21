using System;
using System.Web;
using System.Web.Security;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Data.Linq;

namespace TeamProject
{
    public partial class Instructor1 : System.Web.UI.Page
    {
        KarateSchoolDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1).mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateSchoolDataContext(conn);

            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() != "Instructor")
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
                    
                    string userID = HttpContext.Current.Session["userID"].ToString().Trim();

                    LoginName1 = ;
                    

                    var InstructorQuery = from inst in dbcon.Instructors
                                      join user in dbcon.NetUsers on inst.InstructorID equals user.UserID
                                      join section in dbcon.Sections on inst.InstructorID equals section.Instructor_ID
                                      select new
                                      {
                                          SectionName = section.SectionName,
                                          InstructorFirstName = inst.InstructorFirstName,
                                          InstructorLastName = inst.InstructorLastName
                                      };


                    GridView1.DataSource = InstructorQuery;
                    GridView1.DataBind();


                }
            }
        }
    }
}