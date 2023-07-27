<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main class="fullscreen">
        <div class="brand">
          <div class="name">
            <img src="img/logo.png"> 
            <p>Equipment Management<br> and Maintenance System</p>
          </div>
        </div>

        <div class="card login">
            <div class="card-header">
                <div class="card-text">
                    <h3 class="card-title">Login</h3>
                </div>
                <div class="card-category">Enter username & password to login!</div>
            </div>
            <div class="card-body">
                <div class="mb-2">
                    <label class="sr-only" for="ctEmail">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" class="form-input" placeholder="Username" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <label class="sr-only" for="ctPass">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-input" placeholder="Password" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <asp:Button ID="btnLogin" runat="server" Text="OK" class="btn btn-light" OnClick="btnLogin_Click" />
                </div>
                <div class="mb-2">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </main>
</asp:Content>

