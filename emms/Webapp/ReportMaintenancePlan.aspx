<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportMaintenancePlan.aspx.cs" Inherits="ReportMaintenancePlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Maintenance plan report</h3>
                        </div>
                        <div class="action" style="margin-top: 20px">
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
                                        AutoPostBack="true" OnSelectedIndexChanged="dlFilterNhomTrangThietBi_SelectedIndexChanged" />
                                </div>
                                <div class="filter-item">
                                    Loại xe
                                    <asp:DropDownList ID="dlFilterLoaiTrangThietBi" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Biển số
                                    <asp:TextBox ID="txtFilterBienSo" runat="server" CssClass="form-input" />
                                </div>
                                <div class="w-100"></div>
                                <div class="filter-item" style="display: none">
                                    <span style="display: inline-block; font-style: italic; width: 230px">Tính tần suất trong khoảng:</span>
                                    Từ ngày 
                                    <asp:TextBox ID="txtTanSuatTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item" style="display: none">
                                    đến ngày 
                                    <asp:TextBox ID="txtTanSuatDenNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item" style="display: none">
                                    <asp:Button ID="btnPreviousDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrevious_Click" Text="<<"
                                        Width="40px" ToolTip="Bấm để lùi lại 1 tháng" />
                                    <asp:Button ID="btnNextDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnNext_Click" Text=">>"
                                        Width="40px" ToolTip="Bấm để tăng thêm 1 tháng" />
                                </div>
                                <div class="w-100"></div>
                                <div class="filter-item">
                                    <span style="display: inline-block; font-style: italic; width: 230px">Kế hoạch bảo dưỡng trong khoảng:</span> Từ ngày 
                                    <asp:TextBox ID="txtKeHoachTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtKeHoachDenNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnKeHoachTuNgayPrevious" runat="server" CssClass="btn btn-info btn-sm"
                                        OnClick="btnKeHoachTuNgayPrevious_Click" Text="<<"
                                        Width="40px" ToolTip="Bấm để lùi lại 1 tháng" />
                                    <asp:Button ID="btnKeHoachDenNgayNext" runat="server" CssClass="btn btn-info btn-sm"
                                        OnClick="btnKeHoachDenNgayNext_Click" Text=">>"
                                        Width="40px" ToolTip="Bấm để tăng thêm 1 tháng" />
                                </div>
                                <div class="w-100"></div>
                                <div class="filter-item">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnFilter_Click" Text="Filter" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnClear_Click" Text="Clear" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnExport_Click"
                                        Text="Export" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowDataBound="gridView_RowDataBound"
                            ShowFooter="false" CssClass="table table-striped table-hover"  OnRowCreated="GridView_RowCreated"
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
                                <asp:TemplateField SortExpression="NhomXe" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Nhóm xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("NhomXe") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LoaiTrangThietBi" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LoaiTrangThietBi") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenXe" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tên xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenXe") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BienSo" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlBienSo" runat="server" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LoaiBaoDuong" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại bảo dưỡng</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LoaiBaoDuong") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField SortExpression="NgayBaoDuongGanNhat" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Ngày bảo dưỡng gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.Format("{0: dd/MM/yyyy}", Eval("NgayBaoDuongGanNhat")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoKmGioDaChay" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Số km/giờ lần bảo dưỡng gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoKmGioDaChay" runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField SortExpression="NgayNhapGanNhat" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Ngày nhập số liệu gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.Format("{0: dd/MM/yyyy}", Eval("NgayNhapGanNhat")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoKmGioNhapGanNhat" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Số km/giờ lần nhập gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoKmGioNhapGanNhat" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MocBaoDuongTiepTheo" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Mốc bảo dưỡng tiếp theo</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMocBaoDuongTiepTheo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TanSuatHoatDong" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tần suất hoạt động / ngày</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTanSuatHoatDong" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NgayBaoDuongTiepTheo" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Ngày bảo dưỡng dự kiến tiếp theo</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.Format("{0: dd/MM/yyyy}", Eval("NgayBaoDuongTiepTheo")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SoNgayConLaiChoLanBaoDuongTiepTheo" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Số ngày dự kiến còn lại</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SoNgayConLaiChoLanBaoDuongTiepTheo") %>
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


