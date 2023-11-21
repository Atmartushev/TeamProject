<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="TeamProject.Administrator.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            <h2>Members</h2>
            <asp:GridView ID="gvMembers" runat="server" CssClass="table table-striped table-bordered table-responsive w-75" Height="350">
            </asp:GridView>

            <!-- Add Member Form -->
            <h3>Add Member</h3>
            <div class="form-group">
                <label for="txtMemberUsername">Username:</label>
                <asp:TextBox ID="txtMemberUsername" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberUsername" runat="server" ControlToValidate="txtMemberUsername" ErrorMessage="Username is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <div class="form-group">
                <label for="txtMemberPassword">Password:</label>
                <asp:TextBox ID="txtMemberPassword" runat="server" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberPassword" runat="server" ControlToValidate="txtMemberPassword" ErrorMessage="Password is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <div class="form-group">
                <label for="txtMemberFirstName">First Name:</label>
                <asp:TextBox ID="txtMemberFirstName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberFirstName" runat="server" ControlToValidate="txtMemberFirstName" ErrorMessage="First Name is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <div class="form-group">
                <label for="txtMemberLastName">Last Name:</label>
                <asp:TextBox ID="txtMemberLastName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberLastName" runat="server" ControlToValidate="txtMemberLastName" ErrorMessage="Last Name is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <div class="form-group">
                <label for="txtMemberDateJoined">Date Joined:</label>
                <asp:TextBox ID="txtMemberDateJoined" runat="server" CssClass="form-control" placeholder="MM/DD/YYYY" />
                <!-- You may consider adding a RegularExpressionValidator for date validation -->
            </div>
            <div class="form-group">
                <label for="txtMemberPhoneNumber">Phone Number:</label>
                <asp:TextBox ID="txtMemberPhoneNumber" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberPhoneNumber" runat="server" ControlToValidate="txtMemberPhoneNumber" ErrorMessage="Phone Number is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <div class="form-group">
                <label for="txtMemberEmail">Email:</label>
                <asp:TextBox ID="txtMemberEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMemberEmail" runat="server" ControlToValidate="txtMemberEmail" ErrorMessage="Email is required." CssClass="text-danger" ValidationGroup="MemberValidation" />
            </div>
            <!-- Add other member fields as needed -->
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" CssClass="btn btn-primary" OnClick="btnAddMember_Click" ValidationGroup="MemberValidation" />
            <br /><br />
            <!-- Delete Member Section -->
            <h3>Delete Member</h3>
            <div class="form-group">
                <label for="ddlMembers">Select Member:</label>
                <asp:DropDownList ID="ddlMembers" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMembers_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnDeleteMember" runat="server" Text="Delete Member" CssClass="btn btn-danger" OnClick="btnDeleteMember_Click" />
            <br /><br />
            <h3>Add Member to Section</h3>
            <div class="form-group">
                <label for="ddlAllMembers">Select Member:</label>
                <asp:DropDownList ID="ddlAllMembers" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlMemberSection">Select Section:</label>
                <asp:DropDownList ID="ddlMemberSection" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnMemberToSection" runat="server" Text="Add Member to Section" CssClass="btn btn-primary" OnClick="btnMemberToSection_Click" />

            <br />
            <br />
            <br />
         <hr/> 
        <br />
        <br />
            </div>

        
    <div class="row">
        <div class="col-md-12">
            <h2>Instructors</h2>
            <asp:GridView ID="gvInstructors" runat="server" CssClass="table table-striped table-bordered table-responsive">
            </asp:GridView>

           

            <!-- Add Instructor Form -->
            <h3>Add Instructor</h3>
            <div class="form-group">
                <label for="txtInstructorUsername">Username:</label>
                <asp:TextBox ID="txtInstructorUsername" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvInstructorUsername" runat="server" ControlToValidate="txtInstructorUsername" ErrorMessage="Username is required." CssClass="text-danger" ValidationGroup="InstructorValidation" />
            </div>
            <div class="form-group">
                <label for="txtInstructorPassword">Password:</label>
                <asp:TextBox ID="txtInstructorPassword" runat="server" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvInstructorPassword" runat="server" ControlToValidate="txtInstructorPassword" ErrorMessage="Password is required." CssClass="text-danger" ValidationGroup="InstructorValidation" />
            </div>
            <div class="form-group">
                <label for="txtInstructorFirstName">First Name:</label>
                <asp:TextBox ID="txtInstructorFirstName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvInstructorFirstName" runat="server" ControlToValidate="txtInstructorFirstName" ErrorMessage="First Name is required." CssClass="text-danger" ValidationGroup="InstructorValidation" />
            </div>
            <div class="form-group">
                <label for="txtInstructorLastName">Last Name:</label>
                <asp:TextBox ID="txtInstructorLastName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvInstructorLastName" runat="server" ControlToValidate="txtInstructorLastName" ErrorMessage="Last Name is required." CssClass="text-danger" ValidationGroup="InstructorValidation" />
            </div>
            <div class="form-group">
                <label for="txtInstructorPhoneNumber">Phone Number:</label>
                <asp:TextBox ID="txtInstructorPhoneNumber" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvInstructorPhoneNumber" runat="server" ControlToValidate="txtInstructorPhoneNumber" ErrorMessage="Phone Number is required." CssClass="text-danger" ValidationGroup="InstructorValidation" />
            </div>
            <!-- Add other instructor fields as needed -->
            <asp:Button ID="btnAddInstructor" runat="server" Text="Add Instructor" CssClass="btn btn-primary" OnClick="btnAddInstructor_Click" ValidationGroup="InstructorValidation" />
                        <h3>Delete Instructor</h3>
            <div class="form-group">
                <label for="ddlInstructors">Select Instructor:</label>
                <asp:DropDownList ID="ddlInstructors" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlInstructors_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnDeleteInstructor" runat="server" Text="Delete Instructor" CssClass="btn btn-danger" OnClick="btnDeleteInstructor_Click" />
        </div>
        </div>
        </div>


</asp:Content>