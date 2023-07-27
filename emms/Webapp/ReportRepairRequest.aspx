<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportRepairRequest.aspx.cs" Inherits="ReportRepairRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Repair & Overcom Problem Request Report</h3>
                        </div>
                        <div class="filter" style="margin-top: 20px">
                            <div class="form-inline" id="divFilter" runat="server">
                                <div class="filter-item">
                                    <span style="display: inline-block; font-style: italic; width: 180px">Ngày nhập thẻ sửa chữa:</span> Từ ngày
                                    <asp:TextBox ID="txtFilterNgayNhapTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtFilterNgayNhapDenNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="w-100"></div>
                                <div class="filter-item">
                                    <span style="display: inline-block; font-style: italic; width: 180px">Ngày sửa chữa:</span> Từ ngày
                                    <asp:TextBox ID="txtFilterNgaySuaChuaTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtFilterNgaySuaChuaDenNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="w-100"></div>
                                <div class="filter-item">
                                    Công ty
                                    <asp:DropDownList ID="dlFilterCompany" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Kiểu bảo dưỡng
                                    <asp:DropDownList ID="dlFilterKieuBaoDuong" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Nhóm xe
                                    <asp:DropDownList ID="dlFilterNhomTrangThietBi" runat="server" CssClass="form-input" AutoPostBack="true" OnSelectedIndexChanged="dlFilterNhomTrangThietBi_SelectedIndexChanged" />
                                </div>
                                <div class="filter-item">
                                    Loại xe
                                    <asp:DropDownList ID="dlFilterLoaiTrangThietBi" runat="server" CssClass="form-input" />
                                </div>
                                <div class="w-100"></div>
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
                            </div>
                        </div>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            OnRowDataBound="gridView_RowDataBound" ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField SortExpression="RowNum" HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("STT") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MaThe" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Mã phiếu</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpMaPhieu" runat="server" Target="_blank"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NgayBaoDuong" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Ngày sửa chữa</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.Format("{0:dd/MM/yyyy}",Eval("NgaySuaChua")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="KieuBaoDuong" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                                    <HeaderTemplate>Kiểu bảo dưỡng</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("KieuBaoDuong") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenNhom" HeaderStyle-Width="270px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Nhóm xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenNhom") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LoaiTrangThietBi" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LoaiTrangThietBi") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TenTrangThietBi" HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tên xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TenTrangThietBi") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BienSo" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:HyperLink ID="hpBienSo" runat="server" Target="_blank"/>
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
        function DateControl(el) {
            //confirm("Press a button!");
            var dateFormat = 'DD/MM/YYYY';
            var dateMask = new IMask(el, {
                mask: Date,
                pattern: dateFormat,
                lazy: false,

                format: function (date) {
                    return moment(date).format(dateFormat);
                },
                parse: function (str) {
                    return moment(str, dateFormat);
                },

                blocks: {
                    YYYY: {
                        mask: IMask.MaskedRange,
                        from: 1900,
                        to: 9999
                    },
                    MM: {
                        mask: IMask.MaskedRange,
                        from: 1,
                        to: 12,
                        maxLength: 2
                    },
                    DD: {
                        mask: IMask.MaskedRange,
                        from: 1,
                        to: 31,
                        maxLength: 2
                    }
                }
            });
        }

        //hight light row in gridview
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {
            UpperBound = parseInt('<%= this.gridView.Rows.Count %>') - 1;
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


