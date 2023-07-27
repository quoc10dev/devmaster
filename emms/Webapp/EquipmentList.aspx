<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EquipmentList.aspx.cs" Inherits="EquipmentList" %>

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
                            <h3 class="card-title">Equipment list</h3>
                        </div>
                        <div class="action">
                            <button type="button" class="btn btn-secondary" onclick="OpenPageDetail()" id="btnAddNew" runat="server">
                                <i class="material-icons">add</i> Add</button>
                        </div>
                        <div class="filter">
                            <div class="form-inline">
                                <div class="filter-item">
                                   Công ty
                                    <asp:DropDownList ID="dlFilterCompany" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Nhóm xe
                                    <asp:DropDownList ID="dlFilterNhomTrangThietBi" runat="server" CssClass="form-input" 
                                        AutoPostBack="true" OnSelectedIndexChanged="dlFilterNhomTrangThietBi_SelectedIndexChanged"/>
                                </div>
                                <div class="filter-item">
                                    Loại xe
                                    <asp:DropDownList ID="dlFilterLoaiTrangThietBi" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Biển số
                                    <asp:TextBox ID="txtFilterBienSo" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Tên xe
                                    <asp:TextBox ID="txtFilterTen" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnFilter_Click" Text="Filter" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnClear_Click" Text="Clear" />
                                </div>
                                 <div class="filter-item">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnExport_Click" Text="Export" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting"
                            OnRowCommand="gvList_RowCommand" OnRowDataBound="gvList_RowDataBound" ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField SortExpression="STT" HeaderStyle-Width="30px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("STT") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField SortExpression="TenNhom" HeaderStyle-Width="250px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Nhóm xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenNhom") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenLoaiTrangThietBi" HeaderStyle-Width="220px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenLoaiTrangThietBi") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Ten" HeaderStyle-Width="180px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tên xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Ten") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BienSo" HeaderStyle-Width="110px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("BienSo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField SortExpression="MaTaiSan" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Mã tài sản</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("MaTaiSan") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField SortExpression="NgayDangKiemLanDau" HeaderStyle-Width="70px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Ngày đăng kiểm lần đầu</HeaderTemplate>
                                    <ItemTemplate>
                                         <%# string.Format("{0: dd/MM/yyyy}", Eval("NgayDangKiemLanDau")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoThangDangKiemDinhKy" HeaderStyle-Width="70px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Số tháng đăng kiểm định kỳ</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SoThangDangKiemDinhKy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-right" HeaderStyle-Width="70px"
                                    HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>Actions</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn btn-success btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="editRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="View detail || Edit">
                                            <i class="material-icons">edit</i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkDelete" CssClass="btn btn-danger btn-round"
                                            CommandArgument='<%#string.Format("{0}", Eval("ID")) %>' CommandName="deleteRecord" CausesValidation="false"
                                            data-toggle="tooltip" ToolTip="Delete"><i class="material-icons">close</i></asp:LinkButton>
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
            location.href = "EquipmentDetail.aspx";
        }
    </script>
</asp:Content>


