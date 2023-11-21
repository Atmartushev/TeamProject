using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\zjude\\Source\\Repos\\TeamProject\\App_Data\\KarateSchool(1).mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateSchoolDataContext(conn);

            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() != "Member")
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

                    

                    var MemberQuery = from member in dbcon.Members
                                          join user in dbcon.NetUsers on member.Member_UserID equals user.UserID
                                          join section in dbcon.Sections on member.Member_UserID equals section.Member_ID  
                                      select new
                                          {
                                              SectionName = section.SectionName,
                                              InstructorFirstName = member.MemberFirstName,
                                              InstructorLastName = member.MemberLastName,
                                              PaymentDate = section.SectionStartDate,
                                              SectionFee = section.SectionFee
                                          };



                    GridView1.DataSource = MemberQuery;
                    GridView1.DataBind();
                }
            }
            }
    }
}