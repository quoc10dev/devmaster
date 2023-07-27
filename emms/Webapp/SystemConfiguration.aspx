<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SystemConfiguration.aspx.cs" Inherits="SystemConfiguration" %>

<%@ Register Src="~/UserControl/MyGridViewPager.ascx" TagPrefix="uc1" TagName="MyGridViewPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatePanelMasterPage" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <section class="container-fluid">
                <div class="card bg-primary">
                    <asp:UpdatePanel ID="upUserList" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card-header">
                                <div class="card-text">
                                    <h3 class="card-title">System configuration</h3>
                                </div>
                                <div class="filter" style="margin-top: 30px" id="settingEquipmentTitle" runat="server">
                                    <h4 class="card-title">Equipment management</h4>
                                </div>
                                <div class="card-body" id="settingEquipmentBody" runat="server">
                                    <dl class="datalist">
                                        <dt>Phạm vi hiện cảnh báo đăng kiểm trang thiết bị (số ngày)</dt>
                                        <dd>
                                            <asp:TextBox ID="txtSoNgayCanhBaoDangKiem" runat="server" CssClass="form-input tiny quantity" name="quantity" MaxLength="10" />
                                        </dd>
                                        <dt></dt>
                                        <dd>
                                            <asp:Button ID="btnUpdateEquipmentManagement" runat="server" CssClass="btn btn-info btn-sm" Text="Save" OnClick="btnUpdateEquipmentManagement_Click" />
                                        </dd>
                                    </dl>
                                </div>
                                <div class="filter" id="settingMaintenanceTitle" runat="server">
                                    <h4 class="card-title">Maintenance management</h4>
                                </div>
                                <div class="card-body" id="settingMaintenanceBody" runat="server">
                                    <dl class="datalist">
                                        <dt>Phạm vi hiện cảnh báo bảo dưỡng trang thiết bị (số ngày)</dt>
                                        <dd>
                                            <asp:TextBox ID="txtSoNgayCanhBaoBaoDuong" runat="server" CssClass="form-input tiny quantity" name="quantity" MaxLength="10" />
                                        </dd>
                                        <dt>Số ngày gần nhất nhập liệu để tính tần suất</dt>
                                        <dd>
                                            <asp:TextBox ID="txtSoNgayTinhTanSuat" runat="server" CssClass="form-input tiny quantity" name="quantity" MaxLength="10" />
                                        </dd>
                                        <dt></dt>
                                        <dd>
                                             <asp:Button ID="btnUpdateMaintenanceManagement" runat="server" CssClass="btn btn-info btn-sm" Text="Save" OnClick="btnUpdateMaintenanceManagement_Click" />
                                        </dd>
                                    </dl>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


