<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RoleDetail.aspx.cs" Inherits="RoleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="container-fluid">
                <div class="card bg-primary">
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title"><asp:Label ID="lblAction" runat="server"></asp:Label></h3>
                        </div>
                        <div class="action">
                            <button type="button" class="btn btn-light" onclick="OpenWindow()">
                                <i class="material-icons">list</i> Role list</button>
                        </div>
                        <div class="card-body">
                            <dl class="datalist">
                                <dt>Role name</dt>
                                <dd>
                                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-input small" />
                                    <span style="color: red">(*)</span>
                                </dd>
                                <dt>Description</dt>
                                <dd>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-input large w-75" TextMode="MultiLine" Height="150px" MaxLength="1000"/>
                                </dd>
                                <dt>Active</dt>
                                <dd>
                                    <asp:CheckBox ID="chkActive" runat="server" Checked="true" CssClass="form-check-label" />
                                </dd>
                                <dt></dt>
                                <dd>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="OpenWindow()" Text="Back" UseSubmitBehavior="False" />
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </section>
            <script type="text/javascript">
                function OpenWindow() {
                    location.href = "RoleList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

