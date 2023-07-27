<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EquipmentGroupDetail.aspx.cs" Inherits="EquipmentGroupDetail" %>

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
                                <i class="material-icons">list</i> Equipment group list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt>Equipment group name (Vi)<span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Equipment group name (Eng)</dt>
                                <dd>
                                    <asp:TextBox ID="txtNameEng" runat="server" CssClass="form-input large w-75" />
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
                    location.href = "EquipmentGroupList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

