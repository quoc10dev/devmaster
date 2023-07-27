<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportWarningMaintenance.aspx.cs" Inherits="ReportWarningMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Warning maintenance report</h3>
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
                                <asp:TemplateField SortExpression="NhomXe" HeaderStyle-Width="200px" HeaderStyle-CssClass="col"
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
                                <asp:TemplateField SortExpression="NgayBaoDuongGanNhat" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
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
                                        <%--<%# Eval("SoKmGioDaChay") %>--%>
                                        <asp:Label ID="lblSoKmGioDaChay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NgayNhapGanNhat" HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Ngày nhập số liệu gần nhất</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNgayNhapLieuGanNhat" runat="server" />
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
        function OpenPageDetail() {
            location.href = "EquipmentTypeDetail.aspx";
        }

        //Highlight row when click on Gridview
        window.onload = function () {
            HighlightRow('#<%= this.gvList.ClientID %>');
        }
    </script>
</asp:Content>


