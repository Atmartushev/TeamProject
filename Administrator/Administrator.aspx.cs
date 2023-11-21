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
                    Response.Redirect("logon.aspx", true);
                }

            }

            if (!IsPostBack)
            {
                // Bind GridView with Members' information
                BindMembersGrid();

                // Bind instructors' GridView
                BindInstructorsGrid();

                BindMembersDropdown();

                BindInstructorsDropdown();

                BindMemberSections();

                BindSections();
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

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            // Get the values from the text boxes
            string username = txtMemberUsername.Text;
            string password = txtMemberPassword.Text;
            string firstName = txtMemberFirstName.Text;
            string lastName = txtMemberLastName.Text;
            DateTime dateJoined;
            if (DateTime.TryParse(txtMemberDateJoined.Text, out dateJoined))
            {
                // Successfully parsed the date
                string dateAdded = txtMemberDateJoined.Text;
            }
            else
            {
                // Handle the case where the date is not valid
                // You might want to provide user feedback or log an error
                return;
            }

            string phoneNumber = txtMemberPhoneNumber.Text;
            string email = txtMemberEmail.Text;

            // Create a new Member entity
            Member newMember = new Member
            {
                MemberFirstName = firstName,
                MemberLastName = lastName,
                MemberDateJoined = dateJoined,
                MemberPhoneNumber = phoneNumber,
                MemberEmail = email
            };

            // Create a new NetUser entity (assuming you have a User for each Member)
            NetUser newUser = new NetUser
            {
                UserName = username,
                UserPassword = password,
                UserType = "Member" // Assuming "Member" is the UserType for members
            };

            // Add the new NetUser to the context
            dataContext.NetUsers.InsertOnSubmit(newUser);

            // Associate the new Member with the new NetUser
            newMember.NetUser = newUser;

            // Add the new Member to the context
            dataContext.Members.InsertOnSubmit(newMember);

            // Submit changes to the database
            dataContext.SubmitChanges();

            // Optionally, you can rebind your GridView to display the updated data
            BindMembersGrid();
        }

        protected void btnAddInstructor_Click(object sender, EventArgs e)
        {
            // Get the values from the text boxes
            string username = txtInstructorUsername.Text;
            string password = txtInstructorPassword.Text;
            string firstName = txtInstructorFirstName.Text;
            string lastName = txtInstructorLastName.Text;
            string phoneNumber = txtInstructorPhoneNumber.Text;

            // Create a new Instructor entity
            Instructor newInstructor = new Instructor
            {
                InstructorFirstName = firstName,
                InstructorLastName = lastName,
                InstructorPhoneNumber = phoneNumber
            };

            // Create a new NetUser entity (assuming you have a User for each Instructor)
            NetUser newUser = new NetUser
            {
                UserName = username,
                UserPassword = password,
                UserType = "Instructor" // Assuming "Instructor" is the UserType for instructors
            };

            // Add the new NetUser to the context
            dataContext.NetUsers.InsertOnSubmit(newUser);

            // Associate the new Instructor with the new NetUser
            newInstructor.NetUser = newUser;

            // Add the new Instructor to the context
            dataContext.Instructors.InsertOnSubmit(newInstructor);

            // Submit changes to the database
            dataContext.SubmitChanges();

            // Optionally, you can rebind your GridView to display the updated data
            BindInstructorsGrid();




        }

        protected void btnDeleteMember_Click(object sender, EventArgs e)
        {
            // Get the selected member ID from the DropDownList
            int selectedMemberID = int.Parse(ddlMembers.SelectedValue);

            // Find the member in the DataContext
            Member memberToDelete = dataContext.Members.FirstOrDefault(m => m.Member_UserID == selectedMemberID);

            if (memberToDelete != null)
            {
                // Find the associated NetUser record
                NetUser netUserToDelete = dataContext.NetUsers.FirstOrDefault(u => u.UserID == selectedMemberID);

                // Remove the member from the DataContext
                dataContext.Members.DeleteOnSubmit(memberToDelete);

                if (netUserToDelete != null)
                {
                    // Remove the NetUser record from the DataContext
                    dataContext.NetUsers.DeleteOnSubmit(netUserToDelete);
                }

                // Submit changes to the database
                dataContext.SubmitChanges();

                // Rebind GridView and DropDownList
                BindMembersGrid();
                BindMembersDropdown();
            }
            else
            {
                return;
            }
        }

        protected void btnDeleteInstructor_Click(object sender, EventArgs e)
        {
            // Get the selected instructor ID from the DropDownList
            int selectedInstructorID = int.Parse(ddlInstructors.SelectedValue);



            // Find the instructor in the DataContext
            Instructor instructorToDelete = dataContext.Instructors.FirstOrDefault(i => i.InstructorID == selectedInstructorID);

            if (instructorToDelete != null)
            {
                // Find the associated NetUser record
                NetUser netUserToDelete = dataContext.NetUsers.FirstOrDefault(u => u.UserID == selectedInstructorID);

                // Remove the instructor from the DataContext
                dataContext.Instructors.DeleteOnSubmit(instructorToDelete);

                if (netUserToDelete != null)
                {
                    // Remove the NetUser record from the DataContext
                    dataContext.NetUsers.DeleteOnSubmit(netUserToDelete);
                }

                // Submit changes to the database
                dataContext.SubmitChanges();

                // Rebind GridView and DropDownList
                BindInstructorsGrid();
                BindInstructorsDropdown();
            }
            else
            {
                return;
            }
        }

        protected void ddlMembers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlInstructors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BindMembersDropdown()
        {
            var members = dataContext.Members.Select(m => new
            {
                MemberID = m.Member_UserID,
                MemberName = $"{m.MemberFirstName} {m.MemberLastName}"
            }).ToList();

            ddlMembers.DataSource = members;
            ddlMembers.DataTextField = "MemberName";
            ddlMembers.DataValueField = "MemberID";
            ddlMembers.DataBind();
        }

        private void BindMemberSections()
        {
            var members = dataContext.Members.Select(m => new
            {
                MemberID = m.Member_UserID,
                MemberName = $"{m.MemberFirstName} {m.MemberLastName}"
            }).ToList();

            ddlAllMembers.DataSource = members;
            ddlAllMembers.DataTextField = "MemberName";
            ddlAllMembers.DataValueField = "MemberID";
            ddlAllMembers.DataBind();
        }

        private void BindSections()
        {
            var members = dataContext.Sections.Select(s => new
            {
                SectionID = s.SectionID,
                SectionName = $"{s.SectionName}"
            }).Distinct().ToList();

            ddlMemberSection.DataSource = members;
            ddlMemberSection.DataTextField = "SectionName";
            ddlMemberSection.DataValueField = "SectionID";
            ddlMemberSection.DataBind();
        }

        private void BindInstructorsDropdown()
        {
            var instructors = dataContext.Instructors.Select(i => new
            {
                InstructorID = i.InstructorID,
                InstructorName = $"{i.InstructorFirstName} {i.InstructorLastName}"
            }).ToList();

            ddlInstructors.DataSource = instructors;
            ddlInstructors.DataTextField = "InstructorName";
            ddlInstructors.DataValueField = "InstructorID";
            ddlInstructors.DataBind();
        }

        protected void btnMemberToSection_Click(object sender, EventArgs e)
        {
            // Get the selected values from the dropdown lists
            int memberId = Convert.ToInt32(ddlAllMembers.SelectedValue);
            int sectionId = Convert.ToInt32(ddlMemberSection.SelectedValue);
            string sectionName = ddlMemberSection.SelectedItem.Text;

            // Assuming you have a relationship table (e.g., MemberSections) to store the association
            // You may need to adjust this based on your actual data model
            Section newMemberSection = new Section
            {
                Member_ID = memberId,
                SectionID = sectionId,
                SectionName = sectionName,
                Instructor_ID = 11,
                SectionFee = 500,
                SectionStartDate = DateTime.Now
            };

            // Add the new association to the context
            dataContext.Sections.InsertOnSubmit(newMemberSection);

            // Submit changes to the database
            dataContext.SubmitChanges();
        }
    }
}