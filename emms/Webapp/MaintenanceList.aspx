<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceList.aspx.cs" Inherits="MaintenanceList" %>

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
                            <h3 class="card-title">Maintenance item</h3>
                        </div>
                        <div class="action">
                            <button type="button" class="btn btn-secondary" onclick="OpenPageDetail()" id="btnAddNew" runat="server">
                                <i class="material-icons">add</i> Add</button>
                        </div>
                        <div class="filter">
                            <div class="form-inline">
                                <div class="filter-item">
                                    Maintenance group
                                    <asp:DropDownList ID="dlMaintenanceGroup" runat="server" CssClass="form-input" Style="width: 250px !important" />
                                </div>
                                <div class="filter-item" style="display:none !important">
                                    Maintenance name
                                    <asp:TextBox ID="txtFilterName" runat="server" CssClass="form-input" />
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
                        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            OnRowCommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound" ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField SortExpression="RowNum" HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Ten" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Maintenace name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTen" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenEng" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>English name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTenEng" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoThuTuHienThi" HeaderStyle-Width="100px" HeaderStyle-CssClass=""
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>Order display</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoThuTuHienThi" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="text-right"
                                    HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>Actions</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn btn-success btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="editRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="View detail || Edit">
                                            <i class="material-icons">edit</i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkDelete" CssClass="btn btn-danger btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="deleteRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="Delete" OnClientClick="OpenModalDeleteRole()"><i class="material-icons">close</i></asp:LinkButton>
                                        <asp:HiddenField ID="hfId" runat="server" />
                                        <asp:HiddenField ID="hfName" runat="server" />
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
        function OpenPageDetail() {
            location.href = "MaintenanceDetail.aspx";
        }
        function OpenModalDeleteRole() {
            $('#modalDelete').modal('show')
        }
    </script>
</asp:Content>


