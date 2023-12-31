﻿using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TeamProject
{
    public partial class Instructor1 : System.Web.UI.Page
    {
        KarateSchoolDataContext dbcon;
        string conn = ConfigurationManager.ConnectionStrings["KarateSchoolDB"].ConnectionString;
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
                    Response.Redirect("~/logon.aspx", true);
                }
                else
                {

                    string userID = HttpContext.Current.Session["userID"].ToString().Trim();

                    Label1.Text = HttpContext.Current.Session["instructorFirstName"].ToString();
                    Label2.Text = HttpContext.Current.Session["instructorLastName"].ToString();


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