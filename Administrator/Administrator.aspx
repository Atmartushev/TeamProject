<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="TeamProject.Administrator.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
        <asp:GridView ID="gvMembers" runat="server" CssClass="table table-striped table-bordered table-responsive w-75" Height ="350">

        </asp:GridView>
            </div>
        <div class="col-md-4">
    <asp:GridView ID="gvInstructors" runat="server" CssClass="table table-striped table-bordered table-responsive">

        
    </asp:GridView>
            </div>
        </div>
</asp:Content>
