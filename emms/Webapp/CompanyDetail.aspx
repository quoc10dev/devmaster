<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyDetail.aspx.cs" Inherits="CompanyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="container-fluid">
                <div class="card bg-primary">
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title"><asp:Label ID="lblAction" runat="server"></asp:Label></h3>
                        </div>
                        <div class="action">
                            <button type="button" class="btn btn-light" onclick="ReturnPageList()">
                                <i class="material-icons">list</i> Company list</button>
                        </div>
                        <div class="card-body">
                            <dl class="datalist">
                                <dt>Full name <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtTenDayDu" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Short name</dt>
                                <dd>
                                    <asp:TextBox ID="txtTenVietTat" runat="server" CssClass="form-input small" />
                                </dd>
                                <dt>Address</dt>
                                <dd>
                                    <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-input large w-75" TextMode="MultiLine" Height="90px" MaxLength="300"/>
                                </dd>
                                <dt>Telephone</dt>
                                <dd>
                                    <asp:TextBox ID="txtDienThoai" runat="server" CssClass="form-input large w-75" />
                                </dd>
                                <dt>Email</dt>
                                <dd>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input large w-75" />
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
                    location.href = "CompanyList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

