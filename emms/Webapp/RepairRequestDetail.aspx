<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepairRequestDetail.aspx.cs"
    Inherits="RepairRequestDetail" %>

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
                                <i class="material-icons">list</i>Repair request list</button>
                        </div>
                        <div class="card-body" id="formatControl" runat="server">
                            <dl class="datalist">
                                <dt>Mã phiếu</dt>
                                <dd>
                                    <asp:Label ID="lblMaThe" runat="server" Style="color: red; font-weight: bold" /></dd>
                                <dt>Ngày lập phiếu <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:TextBox ID="txtNgayLapPhieu" runat="server" CssClass="form-input date" /></dd>
                                <dt>Nhóm xe <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:DropDownList ID="dlNhomXe" runat="server" CssClass="form-input select2 large w-75"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlNhomXe_SelectedIndexChanged" /></dd>
                                <dt>Loại xe <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:DropDownList ID="dlLoaiTrangThietBi" runat="server" CssClass="form-input select2 large w-75"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlLoaiTrangThietBi_SelectedIndexChanged" />
                                </dd>
                                <dt>Tên xe <span style="color: red">(*)</span></dt>
                                <dd>
                                    <asp:DropDownList ID="dlTrangThietBi" runat="server" CssClass="form-input select2 large w-75"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlTrangThietBi_SelectedIndexChanged" />
                                </dd>
                                <dt>Biển số</dt>
                                <dd>
                                    <asp:Label ID="lblBienSo" runat="server" />
                                </dd>
                                <dt>Model</dt>
                                <dd>
                                    <asp:Label ID="lblModel" runat="server" /></dd>
                                <dt>Serial No</dt>
                                <dd>
                                    <asp:Label ID="lblSerialNo" runat="server" /></dd>
                                <dt>Số máy</dt>
                                <dd>
                                    <asp:Label ID="lblSoMay" runat="server" /></dd>
                                <dt>
                                    <asp:Label ID="lblSoGioHoacKm" runat="server" Text="Số giờ/km hoạt động"/></dt>
                                <dd>
                                    <asp:TextBox ID="txtSoGioHoacKm" runat="server" class="form-input tiny weight" />
                                </dd>
                                <dt>Người thực hiện </dt>
                                <dd>
                                    <asp:TextBox ID="txtNguoiThucHien" runat="server" class="form-input large w-75" />
                                </dd>
                                <dt></dt>
                                <dd>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrint_Click" Text="Print Repair Request" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="ReturnPageList()" Text="Back" UseSubmitBehavior="False" />
                                </dd>
                            </dl>
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
                    location.href = "RepairRequest.aspx";
                }
                function OpenModalDeleteItem() {
                    $('#modalDelete').modal('show')
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

