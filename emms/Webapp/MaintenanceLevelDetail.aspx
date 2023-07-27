<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceLevelDetail.aspx.cs" Inherits="MaintenanceLevelDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:updatepanel id="UpdatePanel1" runat="server" updatemode="Conditional">
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
                                <i class="material-icons">list</i>Maintenance level list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt>Kiểu bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-50">
                                        <asp:DropDownList ID="dlKieuBaoDuong" runat="server" CssClass="form-input select2" 
                                            AutoPostBack="true" OnSelectedIndexChanged="dlKieuBaoDuong_SelectedIndexChanged"/>
                                    </div>
                                </dd>
                                <dt>Loại bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-50">
                                        <asp:DropDownList ID="dlLoaiBaoDuong" runat="server" CssClass="form-input select2" 
                                            AutoPostBack="true"/>
                                    </div>
                                </dd>
                                <dt>Tên cấp bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                     <div class="w-75">
                                        <asp:TextBox ID="txtTenCapBaoDuong" runat="server" CssClass="form-input"/>
                                    </div>
                                </dd>
                                <dt>Tên viết tắt 1<span style="color: red">(*)</span></dt>
                                <dd>
                                     <div class="w-75">
                                        <asp:TextBox ID="txtTenVietTat_ViTri1" runat="server" CssClass="form-input"/>
                                    </div>
                                </dd>
                                <dt>Tên viết tắt 2<span style="color: red">(*)</span></dt>
                                <dd>
                                     <div class="w-75">
                                        <asp:TextBox ID="txtTenVietTat_ViTri2" runat="server" CssClass="form-input"/>
                                    </div>
                                </dd>
                                <dt>Cấp bảo dưỡng<span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdCap1" runat="server" type="radio" name="radio" 
                                            class="form-check-input" GroupName="CapBaoDuong" />Cấp 1
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdCap2" runat="server" type="radio" name="radio" 
                                            class="form-check-input" GroupName="CapBaoDuong" />Cấp 2
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdCap3" runat="server" type="radio" name="radio" 
                                            class="form-check-input" GroupName="CapBaoDuong" />Cấp 3
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdCap4" runat="server" type="radio" name="radio" 
                                            class="form-check-input" GroupName="CapBaoDuong" />Cấp 4
                                    </div>
                                </dd>
                                <dt>Mốc bảo dưỡng (số km / số giờ)<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoLuongMoc" runat="server" class="form-input tiny weight" type="weight"/>
                                    <asp:Label ID="lblDonViTinh" runat="server" Font-Bold="true" />
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
                    location.href = "MaintenanceLevel.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

