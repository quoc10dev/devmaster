<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceChildTypeDetail.aspx.cs" Inherits="MaintenanceChildTypeDetail" %>

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
                                <i class="material-icons">list</i>Maintenance type list</button>
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
                                <dt>Tên loại bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                     <div class="w-75">
                                        <asp:TextBox ID="txtTenLoaiBaoDuong" runat="server" CssClass="form-input"/>
                                    </div>
                                </dd>
                                <dt>Hạn bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoLuongBaoDuongDinhKy" runat="server" class="form-input tiny weight" type="weight"/>
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
                    location.href = "MaintenanceChildType.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

