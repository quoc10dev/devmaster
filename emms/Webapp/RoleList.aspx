<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="RoleList" %>

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
                    <h3 class="card-title">Role list</h3>
                </div>
                <div class="action">
                    <button type="button" class="btn btn-secondary" onclick="OpenAddRole()" id="btnAddNew" runat="server">
                        <i class="material-icons">add</i> Add</button>
                </div>
                <div class="filter">
                    <div class="form-inline">
                        <div class="filter-item">
                            Rolename
                                    <asp:TextBox ID="txtFilterRoleName" runat="server" CssClass="form-input" />
                        </div>
                        <div class="filter-item">
                            Description
                                    <asp:TextBox ID="txtFilterDescription" runat="server" CssClass="form-input" />
                        </div>
                        <div class="filter-item">
                            <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnFilter_Click" Text="Filter" />
                        </div>
                        <div class="filter-item">
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnClear_Click" Text="Clear" />
                        </div>
                    </div>
                </div>
            </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            OnRowCommand="gvRole_RowCommand" OnRowDataBound="gvRole_RowDataBound" ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor=""/>
                            <Columns>
                                <asp:TemplateField SortExpression="RowNum" HeaderStyle-Width="5%" HeaderStyle-CssClass="col" ItemStyle-Width="20px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("RowNum") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="RoleName" HeaderStyle-Width="20%" HeaderStyle-CssClass="col" ItemStyle-Width="60px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Role name</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("RoleName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Description" HeaderStyle-Width="46%" HeaderStyle-CssClass="" ItemStyle-Width="260px"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Description</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Description") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="IsActive" HeaderStyle-Width="12%" HeaderStyle-CssClass="" ItemStyle-Width="60px"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Active</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblActive" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="17%" HeaderStyle-CssClass="text-right"
                                    HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>Actions</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPermission" CssClass="btn btn-info btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="permissRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="Permiss">
                                            <i class="fas fa-key"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn btn-success btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="editRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="View detail || Edit">
                                            <i class="material-icons">edit</i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkDelete" CssClass="btn btn-danger btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="deleteRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="Delete"><i class="material-icons">close</i></asp:LinkButton>
                                        <asp:HiddenField ID="hfIdRole" runat="server" />
                                        <asp:HiddenField ID="hfRoleName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <uc1:MyGridViewPager runat="server" ID="MyGridViewPager" OnCreateClick="PageIndex_Changed" OnSelectIndexChange="PageSize_Changed" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal fade" id="modalDelete" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="width: 400px">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <h5 class="modal-title">Message</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Literal ID="ltrContentDialog" runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hfIdToDelete" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        function OpenAddRole() {
            location.href = "RoleDetail.aspx";
        }
        function OpenModalDeleteRole() {
            $('#modalDelete').modal('show')
        }
    </script>
</asp:Content>


