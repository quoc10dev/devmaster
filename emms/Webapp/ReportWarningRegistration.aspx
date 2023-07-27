<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportWarningRegistration.aspx.cs" Inherits="ReportWarningRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Warning registration report</h3>
                        </div>
                        <div class="filter" style="margin-top: 20px">
                            <div class="form-inline">
                                <div class="filter-item">
                                    Công ty
                                    <asp:DropDownList ID="dlFilterCompany" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Nhóm xe
                                    <asp:DropDownList ID="dlFilterNhomTrangThietBi" runat="server" CssClass="form-input"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlFilterNhomTrangThietBi_SelectedIndexChanged" />
                                </div>
                                <div class="filter-item">
                                    Loại xe
                                    <asp:DropDownList ID="dlFilterLoaiTrangThietBi" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Tên xe
                                    <asp:TextBox ID="txtFilterTenXe" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Biển số
                                    <asp:TextBox ID="txtFilterBienSo" runat="server" CssClass="form-input" />
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
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowDataBound="gridView_RowDataBound"
                            ShowFooter="false" CssClass="table table-striped table-hover" OnRowCreated="GridView_RowCreated"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField SortExpression="STT" HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("STT") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenNhom" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Nhóm xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenNhom") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LoaiXe" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LoaiXe") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenXe" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tên xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenXe") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BienSo" HeaderStyle-Width="115px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlBienSo" runat="server" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NgayDangKiemLanDau" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Ngày đăng kiểm gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNgayDangKiemGanNhat" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoThangDangKiemDinhKy" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Số tháng đăng kiểm định kỳ</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoThangDangKiemDinhKy" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NgayDangKiemTiepTheo" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Ngày đăng kiểm tiếp theo</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.Format("{0: dd/MM/yyyy}", Eval("NgayDangKiemTiepTheo")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoNgayConLai" HeaderStyle-Width="75px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Số ngày còn lại</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SoNgayConLai") %>
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
        function OpenPageDetail() {
            location.href = "EquipmentTypeDetail.aspx";
        }

        //hight light row in gridview
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {
            UpperBound = parseInt('<%= this.gvList.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;
        }

        function SelectRow(CurrentRow, RowIndex) {
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound) return;

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#e1bee7';
                CurrentRow.style.color = '';
            }

            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }

        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;

            if (KeyCode == 40)
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            else if (KeyCode == 38)
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);

            return false;
        }
    </script>
</asp:Content>


