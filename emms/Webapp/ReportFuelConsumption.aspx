<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportFuelConsumption.aspx.cs" Inherits="ReportFuelConsumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:updatepanel id="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Fuel consumption report</h3>
                        </div>
                        <div class="action" style="margin-top:40px">
                            <button type="button" class="btn btn-secondary" onclick="OpenPageDetail()" id="btnAddNew" runat="server" style="display:none">
                                <i class="material-icons">add</i>Export</button>
                        </div>
                        <div class="filter" id="formatControl" runat="server">
                            <div class="form-inline">
                                <div class="filter-item">
                                    Từ ngày 
                                    <asp:TextBox ID="txtFilterTuNgay" runat="server" CssClass="form-input date"  name="date"/>
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtFilterDenNgay" runat="server" CssClass="form-input date" name ="date"/>
                                </div>
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
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowDataBound="gvList_RowDataBound"
                            ShowFooter="false" CssClass="table table-striped table-hover"
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
                                <asp:TemplateField SortExpression="BienSo" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("BienSo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DinhMuc" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Định mức thực tế</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDinhMuc" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DinhMucBanHanh" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Định mức ban hành</HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblDinhMucBanHanh" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField SortExpression="DonViTinh" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Đơn vị tính</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("DonViTinh") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:updatepanel>
        </div>
    </section>
</asp:Content>


