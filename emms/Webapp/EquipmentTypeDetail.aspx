<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EquipmentTypeDetail.aspx.cs" Inherits="EquipmentTypeDetail" %>

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
                                <i class="material-icons">list</i> Equipment type list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt>Equipment group<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:DropDownList ID="dlEquipGroup" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Equipment type name (Vi)<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Equipment type name (Eng)</dt>
                                <dd>
                                    <asp:TextBox ID="txtNameEng" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Đơn vị ghi nhận hoạt động <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdGio" runat="server" type="radio" name="radio" class="form-check-input" GroupName="DonViGhiNhanHoatDong" />Giờ
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rdKm" runat="server" type="radio" name="radio" class="form-check-input" GroupName="DonViGhiNhanHoatDong" />Km
                                    </div>
                                </dd>
                                <dt>Kiểu bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-25">
                                        <asp:DropDownList ID="dlKieuBaoDuong" runat="server" CssClass="form-input select2" OnSelectedIndexChanged="dlKieuBaoDuong_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                </dd>
                                <dt>Loại bảo dưỡng <span style="color: red">(*)</span></dt>
                                <dd>
                                    <div class="w-25">
                                        <asp:DropDownList ID="dlLoaiBaoDuong" runat="server" CssClass="form-input select2" />
                                    </div>
                                </dd>
                                <dt>Number display <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtNumberDisplay" runat="server" CssClass="form-input tiny quantity" name="quantity" />
                                </dd>
                                <dt>Note</dt>
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
                    location.href = "EquipmentTypeList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

