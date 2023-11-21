using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TeamProject
{
    public partial class Member1 : System.Web.UI.Page
    {
        KarateSchoolDataContext dbcon;
        string conn = ConfigurationManager.ConnectionStrings["KarateSchoolDB"].ConnectionString;
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
                    Response.Redirect("~/logon.aspx", true);
                }
                else
                {

                    string userID = HttpContext.Current.Session["userID"].ToString().Trim();

                    Label1.Text = HttpContext.Current.Session["memberFirstName"].ToString();
                    Label2.Text = HttpContext.Current.Session["memberLastName"].ToString();

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