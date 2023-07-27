<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceRequestFormDetail.aspx.cs"
    Inherits="MaintenanceRequestFormDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/MaintenanceListPopupExtender.ascx" TagPrefix="uc1" TagName="MaintenanceListPopupExtender" %>
<%@ Register Src="~/UserControl/TaskListPopupExtender.ascx" TagPrefix="uc1" TagName="TaskListPopupExtender" %>

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
                                <i class="material-icons">list</i>Maintenance request form list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <div class="row datagrid">
                                <div class="col-md-6">
                                    <label>Nhóm trang thiết bị  <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlNhomTrangThietBi" runat="server" CssClass="form-input select2" AutoPostBack="true" OnSelectedIndexChanged="dlNhomTrangThietBi_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Loại trang thiết bị  <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlLoaiTrangThietBi" runat="server" CssClass="form-input select2" AutoPostBack="false" />
                                    </div>
                                </div>
                            </div>
                            <h5 class="mt-5"><b>HẠNG MỤC BẢO DƯỠNG<b /> &nbsp;&nbsp;
                                <asp:Button ID="btnShow" runat="server" Text="Add maintenance" CssClass="btn btn-info btn-sm" OnClick="btnShowPopup_Click" />
                            </h5>
                            <div class="card-body table-responsive">
                                <asp:GridView ID="gvMaintenanceRequest" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                                    OnRowCommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound"
                                    CssClass="table table-striped table-hover" ShowFooter="false" AllowSorting="true"
                                    UseAccessibleHeader="true" GridLines="None">
                                    <HeaderStyle ForeColor="" />
                                    <Columns>
                                        <asp:TemplateField SortExpression="STT" HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>STT</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTT" runat="server" />
                                                <asp:HiddenField ID="hfIDHangMuc" runat="server" />
                                                <asp:HiddenField ID="hfIDHangMucLoaiTTB" runat="server" />
                                                <asp:HiddenField ID="hfIDTaskList" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Ten" HeaderStyle-Width="300px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Hạng mục bảo dưỡng</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTenHangMuc" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Danh sách công việc</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal ID="ltrMoTaCongViec" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkSelectTask" CssClass="btn-sm btn-link"
                                                    CommandName="selectTask" CausesValidation="false"
                                                    data-toggle="tooltip" ToolTip="Select task">
                                                    Chọn công việc
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="ReturnPageList()" Text="Back" UseSubmitBehavior="False" />
                        </div>
                    </div>
                </div>

                <!-- ModalPopupExtender -->
                <cc1:ModalPopupExtender ID="modalPopupExt" runat="server" PopupControlID="Panel1" TargetControlID="btnFake"
                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                    <div style="width: 600px; height: 650px !important">
                        <%--<asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <uc1:MaintenanceListPopupExtender runat="server" ID="maintenanceList" />
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <asp:Button ID="btnClose" runat="server" Text="Close" Style="display: none" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btnFake" runat="server" Style="display: none" />
                <!-- ModalPopupExtender -->

                <!-- ModalPopupExtender -->
                <cc1:ModalPopupExtender ID="modalPopupExtTaskList" runat="server" PopupControlID="Panel2" TargetControlID="btnFake2"
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                    <div style="width: 600px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="Server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc1:TaskListPopupExtender runat="server" ID="selectTaskList" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Button ID="btnFake2" runat="server" Style="display: none" />
                <!-- ModalPopupExtender -->

            </section>

            <script type="text/javascript">
                function ReturnPageList() {
                    location.href = "MaintenanceRequestForm.aspx";
                }
                function OpenSelectEquipmentDialog() {
                    $('#modalSelectEquipmentDialog').modal('show')
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Lưu vị trí scroll của popup -->
    <script type="text/javascript">
        var xPos1, yPos1, xPos2, yPos2;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            var $popup1 = $('#PopupMaintenanceList'),
                $popup2 = $('#PopupTaskList');

            if ($popup1.length > 0) {
                xPos1 = $popup1.scrollLeft();
                yPos1 = $popup1.scrollTop();
            }
            if ($popup2.length > 0) {
                xPos2 = $popup2.scrollLeft();
                yPos2 = $popup2.scrollTop();
            }
        }

        function EndRequestHandler(sender, args) {
            var $popup1 = $('#PopupMaintenanceList'),
                $popup2 = $('#PopupTaskList');

            if ($popup1.length > 0) {
                $popup1.scrollLeft(xPos1);
                $popup1.scrollTop(yPos1);
            }
            if ($popup2.length > 0) {
                $popup2.scrollLeft(xPos2);
                $popup2.scrollTop(yPos2);
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>
