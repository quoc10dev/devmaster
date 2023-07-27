<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EquipmentDetail.aspx.cs" Inherits="EquipmentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="container-fluid">
                <div class="card bg-primary">
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">
                                <asp:Label ID="lblAction" runat="server"></asp:Label></h3>
                        </div>
                        <div class="action">
                            <button type="button" class="btn btn-light" onclick="ReturnPageList()">
                                <i class="material-icons">list</i> Equipment list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt id="dlIdTitle" runat="server" visible="false">ID <span style="color: red">(*)</span></dt>
                                <dd id="ddIdTitle" runat="server" visible="false">
                                    <asp:Label ID="lblID" runat="server" Font-Bold="true" />
                                </dd>
                                <dt>Nhóm xe<span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-50">
                                        <asp:DropDownList ID="dlNhomXe" runat="server" CssClass="form-input select2"
                                            AutoPostBack="true" OnSelectedIndexChanged="dlNhomXe_SelectedIndexChanged" />
                                    </div>
                                </dd>
                                <dt>Loại xe <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-50">
                                        <asp:DropDownList ID="dlLoaiTrangThietBi" runat="server" CssClass="form-input select2"
                                            AutoPostBack="true" OnSelectedIndexChanged="dlLoaiTrangThietBi_SelectedIndexChanged" />
                                    </div>
                                </dd>
                                <dt>Tên xe <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtTen" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Biển số <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtBienSo" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Mã tài sản <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtMaTaiSan" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Năm sản xuất</dt>
                                <dd>
                                    <asp:TextBox ID="txtNamSanXuat" runat="server" CssClass="form-input tiny year" name="year" />
                                </dd>
                                <dt>Nước sản xuất</dt>
                                <dd>
                                    <asp:TextBox ID="txtNuocSanXuat" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Số khung</dt>
                                <dd>
                                    <asp:TextBox ID="txtSoKhung" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Loại máy</dt>
                                <dd>
                                    <asp:TextBox ID="txtLoaiMay" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Số máy</dt>
                                <dd>
                                    <asp:TextBox ID="txtSoMay" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Tài sản thuộc công ty <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-25">
                                        <asp:DropDownList ID="dlCompany" runat="server" CssClass="form-input select2" />
                                    </div>
                                </dd>
                                <dt>Ngày đăng kiểm gần nhất</dt>
                                <dd>
                                    <asp:TextBox ID="txtNgayDangKiemLanDau" runat="server" CssClass="form-input date" name="date" />
                                </dd>
                                <dt>Số tháng đăng kiểm định kỳ</dt>
                                <dd>
                                    <asp:TextBox ID="txtSoThangDangKiemDinhKy" runat="server" CssClass="form-input tiny quantity" name="quantity" />
                                </dd>
                                <dt>Hiện cảnh báo đăng kiểm</dt>
                                <dd>
                                    <asp:CheckBox ID="chkCanhBaoDangKiem" runat="server" />
                                </dd>
                                <dt>Ngày bảo dưỡng gần nhất<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtNgayBaoDuongGanNhat" runat="server" CssClass="form-input date" />
                                </dd>
                                <dt>Số km bảo dưỡng gần nhất<span style="color: red" id="warningKm" runat="server">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoKmBaoDuongGanNhat" runat="server" class="form-input tiny weight" type="weight" />
                                </dd>
                                <dt>Số giờ bảo dưỡng gần nhất<span style="color: red" id="warningGio" runat="server">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoGioBaoDuongGanNhat" runat="server" class="form-input tiny weight" type="weight" />
                                </dd>
                                <dt>Quy trình bảo dưỡng<span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-50">
                                        <asp:DropDownList ID="dlQuyTrinhBaoDuong" runat="server" CssClass="form-input select2" />
                                    </div>
                                </dd>
                                <dt>Bảo dưỡng theo</dt>
                                <dd>
                                    <asp:TextBox ID="txtBaoDuongTheo" runat="server" CssClass="form-input large w-75" MaxLength="1000" />
                                </dd>
                                <dt>Ghi chú</dt>
                                <dd>
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-input large w-75" TextMode="MultiLine" Height="90px" MaxLength="300" />
                                </dd>
                                <dt></dt>
                                <dd>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="ReturnPageList()" Text="Back" UseSubmitBehavior="False" />
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </section>
            <script type="text/javascript">
                function ReturnPageList() {
                    location.href = "EquipmentList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

