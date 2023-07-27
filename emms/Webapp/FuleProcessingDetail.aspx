<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FuleProcessingDetail.aspx.cs" Inherits="FuleProcessingDetail" %>

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
                                <i class="material-icons">list</i> Fule processing list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt id="dlIdTitle" runat="server" visible="false">ID <span style="color: red">(*)</span></dt>
                                <dd id="ddIdTitle" runat="server" visible="false">
                                    <asp:Label ID="lblID" runat="server" Font-Bold="true" />
                                </dd>
                                <dt>Ngày nạp <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtNgayNap" runat="server" CssClass="form-input date" name ="date"/>
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
                                    <div class="w-75">
                                        <asp:DropDownList ID="dlTrangThietBi" runat="server" CssClass="form-input select2"/>
                                    </div>
                                </dd>
                                <dt>Số lượng nạp (lít) <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoLuong" runat="server" class="form-input tiny weight" />
                                </dd>
                                <dt>Ghi chú</dt>
                                <dd>
                                    <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-input large w-75" TextMode="MultiLine" Height="90px" MaxLength="300" />
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
                    location.href = "FuleProcessing.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

