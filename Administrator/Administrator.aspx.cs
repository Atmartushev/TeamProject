using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TeamProject.Administrator
{
    public partial class Administrator : System.Web.UI.Page
    {
        KarateSchoolDataContext dataContext;

        string connectionString = ConfigurationManager.ConnectionStrings["KarateSchoolDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            dataContext = new KarateSchoolDataContext(connectionString);

            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() != "Administrator")
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("Logon.aspx", true);
                }

            }

            if (!IsPostBack)
            {
                // Bind GridView with Members' information
                BindMembersGrid();

                // Bind instructors' GridView
                BindInstructorsGrid();
            }

        }

        private void BindMembersGrid()
        {
            var membersQuery = from member in dataContext.Members
                               join user in dataContext.NetUsers on member.Member_UserID equals user.UserID
                               select new
                               {
                                   member.MemberFirstName,
                                   member.MemberLastName,
                                   member.MemberPhoneNumber,
                                   member.MemberDateJoined
                               };

            gvMembers.DataSource = membersQuery;
            gvMembers.DataBind();

        }

        // Method to bind instructors' GridView
        private void BindInstructorsGrid()
        {
            var instructorsQuery = from instructor in dataContext.Instructors
                                   join user in dataContext.NetUsers on instructor.InstructorID equals user.UserID
                                   select new
                                   {
                                       instructor.InstructorFirstName,
                                       instructor.InstructorLastName,
                                       instructor.InstructorPhoneNumber
                                   };

            gvInstructors.DataSource = instructorsQuery.ToList();
            gvInstructors.DataBind();
        }


    }
}