<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="container-fluid">
                <div class="card bg-primary">
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Your profile</h3>
                        </div>
                        <div class="card-body" style="margin-top:30px">
                            <dl class="datalist">
                                <dt>Username</dt>
                                <dd>
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label></dd>
                                <dt>Full name</dt>
                                <dd>
                                    <asp:Label ID="lblFullname" runat="server"></asp:Label></dd>
                                <dt>Email</dt>
                                <dd>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

