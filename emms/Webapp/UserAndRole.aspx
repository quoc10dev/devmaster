<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserAndRole.aspx.cs" Inherits="UserAndRole" %>

<%@ Register Src="~/UserControl/MyGridViewPager.ascx" TagPrefix="uc1" TagName="MyGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                <div class="card-text">
                    <h3 class="card-title">User and Role</h3>
                </div>
                <div class="action">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info btn-sm" Text="Save" OnClick="btnUpdate_Click"></asp:Button>
                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="OpenWindow()" 
                                        Text="Back" UseSubmitBehavior="False" />
                </div>
                <div class="filter">
                    <div class="form-inline">
                        <div class="filter-item">
                            Username: <asp:Label ID="lblUserName" runat="server" style="font-weight:bold"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            OnRowDataBound="gvRole_RowDataBound" ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor=""/>
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Select</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                         <asp:HiddenField ID="hfIdRole" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="RoleName" HeaderStyle-Width="25%" HeaderStyle-CssClass="col" ItemStyle-Width="60px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Role name</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("RoleName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Description" HeaderStyle-Width="65%" HeaderStyle-CssClass="col" ItemStyle-Width="400px"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Description</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Description") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

    <script type="text/javascript">
        function OpenWindow() {
            location.href = "UserList.aspx";
        }
    </script>
</asp:Content>


