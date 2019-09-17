<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRole.aspx.cs" Inherits="Aplicada.Account.UserRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Roles By User</h3> 

<p> 
     <b>Select a User:</b> 
     <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" 
          DataTextField="UserName" DataValueField="UserName"> 

     </asp:DropDownList> 
</p> 
<p> 
     <asp:Repeater ID="UsersRoleList" runat="server"> 
          <ItemTemplate> 
               <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" 

                    Text='<%# Container.DataItem %>' /> 
               <br /> 
          </ItemTemplate> 
     </asp:Repeater> 
</p>
</asp:Content>
