<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FrequencyWorkingDetail.aspx.cs" Inherits="FrequencyWorkingDetail" %>

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
                                <i class="material-icons">list</i> Frequency working list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
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
                                    <div class="w-75">
                                        <asp:DropDownList ID="dlTrangThietBi" runat="server" CssClass="form-input select2" AutoPostBack="true" OnSelectedIndexChanged="dlTrangThietBi_SelectedIndexChanged"/>
                                    </div>
                                </dd>
                                <dt>Tần suất (số giờ/ngày hoặc km/ngày)<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoLuongTanSuat" runat="server" class="form-input tiny weight" type="weight"/>
                                    <asp:Label ID="lblDonViTanSuat" runat="server" Font-Bold="true" />
                                </dd>
                                <dt>Ngày bắt đầu áp dụng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtNgayBatDauApDung" runat="server" CssClass="form-input date" name ="date"/>
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
                    location.href = "FrequencyWorking.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

