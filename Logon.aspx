<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="logon.aspx.cs" Inherits="TeamProject.logon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login</h2>
    
    <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/home.aspx" DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate">
    </asp:Login>
</asp:Content>
