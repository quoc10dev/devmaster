<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceRequestDetail.aspx.cs"
    Inherits="MaintenanceRequestDetail" %>

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
                                <i class="material-icons">list</i>Maintenance request list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <div class="row datagrid">
                                <div class="col-md-6">
                                    <label>Mã thẻ</label>
                                    <div>
                                        <asp:Label ID="lblMaThe" runat="server" Style="color: red; font-weight: bold" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Ngày lập phiếu <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:TextBox ID="txtNgayLapPhieu" runat="server" CssClass="form-input date" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Nhóm xe <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlNhomXe" runat="server" CssClass="form-input select2"
                                            AutoPostBack="true" OnSelectedIndexChanged="dlNhomXe_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Loại xe <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlLoaiTrangThietBi" runat="server" CssClass="form-input select2"
                                            AutoPostBack="true" OnSelectedIndexChanged="dlLoaiTrangThietBi_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Tên xe <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlTrangThietBi" runat="server" CssClass="form-input select2"
                                            AutoPostBack="true" OnSelectedIndexChanged="dlTrangThietBi_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Cấp bảo dưỡng <span style="color: red">(*)</span></label>
                                    <div>
                                        <asp:DropDownList ID="dlCapBaoDuong" runat="server" CssClass="form-input select2" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Biển số</label>
                                    <div>
                                        <asp:Label ID="lblBienSo" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Model</label>
                                    <div>
                                        <asp:Label ID="lblModel" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Serial No</label>
                                    <div>
                                        <asp:Label ID="lblSerialNo" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Số máy</label>
                                    <div>
                                        <asp:Label ID="lblSoMay" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        <asp:Label ID="lblSoGioHoacKm" runat="server" /></label>
                                    <div>
                                        <asp:Label ID="lblSoGioHoacSoKm" runat="server" Visible="false" />
                                        <asp:TextBox ID="txtSoGioHoacKm" runat="server" class="form-input tiny weight" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Người thực hiện </label>
                                    <div>
                                        <asp:TextBox ID="txtNguoiThucHien" runat="server" class="form-input" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Trạng thái </label>
                                    <div>
                                        <asp:DropDownList ID="dlState" runat="server" CssClass="form-input select2" />
                                    </div>
                                </div>
                            </div>
                            <h5 class="mt-5"><b>HẠNG MỤC BẢO DƯỠNG<b /></h5>
                            <div class="card-body table-responsive">
                                <asp:GridView ID="gvMaintenanceRequest" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                                    OnRowCommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound"
                                    CssClass="table table-striped table-hover" ShowFooter="false" AllowSorting="true"
                                    UseAccessibleHeader="true" GridLines="None">
                                    <HeaderStyle ForeColor="" />
                                    <Columns>
                                        <asp:TemplateField SortExpression="STT" HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>STT</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTT" runat="server" />
                                                <asp:HiddenField ID="hfIDHangMuc" runat="server" />
                                                <asp:HiddenField ID="hfChiTietBaoDuong" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Ten" HeaderStyle-Width="300px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Hạng mục bảo dưỡng</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTenHangMuc" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Level1" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskListLevel1" runat="server" />
                                                <asp:HiddenField ID="hfIdTaskLevel1" runat="server" />
                                                <asp:HiddenField ID="hfIdMaintenanceLevel1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Level2" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskListLevel2" runat="server" />
                                                <asp:HiddenField ID="hfIdTaskLevel2" runat="server" />
                                                <asp:HiddenField ID="hfIdMaintenanceLevel2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Level3" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskListLevel3" runat="server" />
                                                <asp:HiddenField ID="hfIdTaskLevel3" runat="server" />
                                                <asp:HiddenField ID="hfIdMaintenanceLevel3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Level4" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskListLevel4" runat="server" />
                                                <asp:HiddenField ID="hfIdTaskLevel4" runat="server" />
                                                <asp:HiddenField ID="hfIdMaintenanceLevel4" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="IsChecked" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>Kiểm tra công việc thực hiện</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsChecked" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="IsRequiresRepair" HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                                            <HeaderTemplate>Yêu cầu sửa chữa</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsRequiresRepair" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrint_Click" Text="Print Request Form" />
                            <asp:Button ID="btnPrintAcceptanceForm" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrintAcceptanceForm_Click" Text="Print Acceptance Form" Visible="false" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="ReturnPageList()" Text="Back" UseSubmitBehavior="False" />
                        </div>
                        <div class="col-12" style="margin-top: 40px">
                            <asp:UpdatePanel ID="upUploadFile" runat="server">
                                <ContentTemplate>
                                    <h5 class="mt-5"><b>FILE ĐÍNH KÈM<b /></h5>
                                    <div style="font-style: normal!important">
                                        <asp:Label ID="lblCurrentFile" runat="server" /><br />
                                        <asp:FileUpload ID="fileUpload" runat="server" ToolTip="Bấm để chọn file đính kèm" Style="margin-top: 10px" />
                                        <asp:Literal ID="ltrFileName" runat="server" />
                                    </div>
                                    <div class="filter-item" style="margin-top: 10px">
                                        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click"
                                            Text="Upload" CssClass="btn btn-info btn-sm" ToolTip="Bấm để upload file" />
                                        <asp:Button ID="btnViewFile" runat="server" OnClick="btnViewFile_Click"
                                            Text="View file" CssClass="btn btn-info btn-sm" ToolTip="Bấm để xem file" />
                                        <asp:Button ID="btnDeleteAttachFile" runat="server" OnClick="btnCheckBeforeDelete_Click"
                                            Text="Delete" CssClass="btn btn-info btn-sm" ToolTip="Bấm để xóa file đính kèm" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal fade" id="modalDelete" role="dialog" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered" style="width: 400px">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h5 class="modal-title">Message</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Literal ID="ltrContentDialog" runat="server"></asp:Literal>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="btnDeleteAttachFile_Click" />
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <script type="text/javascript">
                function ReturnPageList() {
                    location.href = "MaintenanceRequest.aspx";
                }
                function OpenSelectEquipmentDialog() {
                    $('#modalSelectEquipmentDialog').modal('show')
                }
                function OpenModalDeleteItem() {
                    $('#modalDelete').modal('show')
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

