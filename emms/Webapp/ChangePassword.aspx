<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="container-fluid">
                <div class="card bg-primary">
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Change password</h3>
                        </div>
                        <div class="card-body" style="margin-top:30px">
                            <dl class="datalist">
                                <dt>Username</dt>
                                <dd>
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label></dd>
                                <dt>Full name</dt>
                                <dd>
                                    <asp:Label ID="lblFullname" runat="server"></asp:Label></dd>
                                <dt>Current password</dt>
                                <dd>
                                    <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-input small" TextMode="Password" Font-Size="30px"/>
                                    <span style="color: red">(*)</span>
                                </dd>
                                <dt>New password</dt>
                                <dd>
                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-input small" TextMode="Password" Font-Size="30px"/>
                                    <span style="color: red">(*)</span>
                                </dd>
                                <dt>Confirm new password</dt>
                                <dd>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-input small" TextMode="Password" Font-Size="30px"/>
                                    <span style="color: red">(*)</span>
                                </dd>
                                <dt></dt>
                                <dd>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Change" />
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

