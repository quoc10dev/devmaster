<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="UserList" %>

<%@ Register Src="~/UserControl/MyGridViewPager.ascx" TagPrefix="uc1" TagName="MyGridViewPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatePanelMasterPage" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <section class="container-fluid">
                <div class="card bg-primary">
                    <asp:UpdatePanel ID="upUserList" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card-header">
                                <div class="card-text">
                                    <h3 class="card-title">User list</h3>
                                </div>
                                <div class="action">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-secondary" Text="Add" OnClick="btnAdd_Click" Visible="false"></asp:Button>
                                    <asp:LinkButton ID="lnkAdd" runat="server" CssClass="btn btn-secondary" Visible="false"><i class="material-icons">add</i>Add</asp:LinkButton>
                                    <button type="button" class="btn btn-secondary" onclick="OpenAddUser()" id="btnAddNew" runat="server">
                                        <i class="material-icons">add</i> Add</button>
                                </div>
                                <div class="filter">
                                    <div class="form-inline">
                                        <div class="filter-item">
                                            Username
                                    <asp:TextBox ID="txtFilterUserName" runat="server" CssClass="form-input" />
                                        </div>
                                        <div class="filter-item">
                                            Fullname
                                    <asp:TextBox ID="txtFilterFullname" runat="server" CssClass="form-input" />
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
                                <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                                    AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                                    OnRowCommand="gvUser_RowCommand" OnRowDataBound="gvUser_RowDataBound" ShowFooter="false"
                                    CssClass="table table-striped table-hover"
                                    UseAccessibleHeader="true" GridLines="None">
                                    <HeaderStyle ForeColor="" />
                                    <Columns>
                                        <asp:TemplateField SortExpression="RowNum" HeaderStyle-Width="5%" HeaderStyle-CssClass="col" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>No.</HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("RowNum") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Username" HeaderStyle-Width="15%" HeaderStyle-CssClass="col" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Username</HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Username") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Fullname" HeaderStyle-CssClass="col" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Full name</HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Fullname") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Email" HeaderStyle-CssClass="col" HeaderStyle-Width="25%"
                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Email</HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Email") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="IsActive" HeaderStyle-Width="12%" HeaderStyle-CssClass="col" 
                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Active</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblActive" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-right" HeaderStyle-Width="18%" 
                                                HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right">
                                            <HeaderTemplate>Actions</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkPermission" CssClass="btn btn-info btn-round"
                                                    CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="permissRecord" CausesValidation="false"
                                                    data-toggle="tooltip" ToolTip="Permiss">
                                            <i class="fas fa-user-lock"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn btn-success btn-round"
                                                    CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="editRecord" CausesValidation="false"
                                                    data-toggle="tooltip" ToolTip="View detail || Edit">
                                            <i class="material-icons">edit</i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkDelete" CssClass="btn btn-danger btn-round"
                                                    CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="deleteRecord" CausesValidation="false"
                                                    data-toggle="tooltip" ToolTip="Delete"><i class="material-icons">close</i></asp:LinkButton>
                                                <asp:HiddenField ID="hfIdUser" runat="server" />
                                                <asp:HiddenField ID="hfUsername" runat="server" />
                                                <asp:HiddenField ID="hfFullname" runat="server" />
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
                                            <asp:HiddenField ID="hfIdUserToDelete" runat="server" />
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
                function OpenAddUser() {
                    location.href = "UserDetail.aspx";
                }

                function OpenModalDeleteUser() {
                    $('#modalDelete').modal('show')
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


