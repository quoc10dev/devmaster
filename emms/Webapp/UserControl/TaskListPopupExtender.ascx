<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskListPopupExtender.ascx.cs" Inherits="UserControl_TaskListPopupExtender" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Chọn công việc cho từng cấp bảo dưỡng</h5>
                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>
                </div>
                <div class="modal-body" style="margin-bottom:0px; padding-bottom:0px !important">
                    <div class="row mb-3">
                        <div class="col-md-4" style="text-align:left; font-weight:normal">Tên hạng mục:</div>
                        <div class="col-md-8" style="text-align:left">
                            <asp:Label ID="lblMaintennanceName" runat="server" style="text-align:left"/>
                        </div>
                        <div class="col-md-4" style="text-align:left; font-weight:normal; margin-top:15px">Cấp bảo dưỡng <span style="color: red">(*)</span> :</div>
                        <div class="col-md-8" style="text-align:left; font-weight:normal; margin-top:8px">
                            <asp:DropDownList ID="dlCapBaoDuong" runat="server" CssClass="form-input" AutoPostBack="true"
                                OnSelectedIndexChanged="dlCapBaoDuong_SelectedIndexChanged" />
                        </div>
                    </div>
                </div>
                <div class="modal-body" id="PopupTaskList" clientidmode="static" runat="server" 
                    style="max-height: 420px; overflow: auto; margin-top:0px; padding-top:0px !important">
                    <asp:Label ID="selectedValue" runat="server" />
                    <asp:HiddenField ID="hfCurrentRowIndex" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfParentContainer" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfColumnValue" runat="server"></asp:HiddenField>

                    <asp:GridView ID="gvTaskList" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                        AllowSorting="true" ShowFooter="false" CssClass="table table-bordered table-striped table-hover"
                        OnRowCreated="gvTaskList_RowCreated" OnRowDataBound="gvTaskList_RowDataBound"
                        UseAccessibleHeader="true" GridLines="None">
                        <HeaderStyle ForeColor="" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <HeaderTemplate>Mã</HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("MaCongViec") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <HeaderTemplate>Tên công việc</HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("TenCongViec") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <HeaderTemplate>Chọn</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server"
                                        OnCheckedChanged="chkSelect_OnCheckedChanged" AutoPostBack="true" />
                                    <asp:HiddenField ID="hfIDCongViec" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-info" OnClick="btnSelect_Click" Text="Select" />
                    <%--OnClientClick="ClosePopup()"--%>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" style="display: none">Close</button>
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<style>
    .select2-dropdown {
        z-index: 10002;
    }
</style>
<script>
    var SelectedRow = null;
    var SelectedRowIndex = null;
    var UpperBound = null;
    var LowerBound = null;

    window.onload = function () {
        UpperBound = parseInt('<%= this.gvTaskList.Rows.Count %>') - 1;
        LowerBound = 0;
        SelectedRowIndex = -1;
        RetainSelectedRow1();
    }

    function SelectRow1(CurrentRow, RowIndex) {
        if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
            return;

        if (SelectedRow != null) {
            SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
            SelectedRow.style.color = SelectedRow.originalForeColor;
        }

        if (CurrentRow != null) {
            CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
            CurrentRow.originalForeColor = CurrentRow.style.color;
            CurrentRow.style.backgroundColor = '#9c27b04a';
        }

        SelectedRow = CurrentRow;
        SelectedRowIndex = RowIndex;

        $('#<%= selectedValue.ClientID %>').text(RowIndex);
        $('#<%= hfParentContainer.ClientID %>').val(CurrentRow);
        $('#<%= hfCurrentRowIndex.ClientID %>').val(RowIndex);

        $('#<%= hfColumnValue.ClientID %>').val(CurrentRow.cells[1].innerHTML);

        setTimeout("SelectedRow.focus();", 0);
    }

    function RetainSelectedRow1() {
        var parent = $('#<%= hfParentContainer.ClientID %>').val();
        var currentIndex = $('#<%= hfCurrentRowIndex.ClientID %>').val();
        if (parent != null) {
            SelectRow1(parent, currentIndex);
        }
    }

    function SelectSibling1(e) {
        var e = e ? e : window.event;
        var KeyCode = e.which ? e.which : e.keyCode;

        if (KeyCode == 40)
            SelectRow1(SelectedRow.nextSibling, SelectedRowIndex + 1);
        else if (KeyCode == 38)
            SelectRow1(SelectedRow.previousSibling, SelectedRowIndex - 1);

        return false;
    }

    function openModal() {
        $(".modal-backdrop").remove();
        $("#modalSelectTaskDialog").modal('show');

    }

    function ClosePopup() {
        $('#modalSelectTaskDialog').modal('hide')
    }
</script>

<!-- Gridview sau khi postback sẽ không thay đổi vị trí -->
<%--<script type="text/javascript">
    // It is important to place this JavaScript code after ScriptManager1
    var xPos2, yPos2;
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    function BeginRequestHandler(sender, args) {
        if ($get('<%=PopupTaskList.ClientID%>') != null) {
            xPos2 = $get('<%=PopupTaskList.ClientID%>').scrollLeft;
            yPos2 = $get('<%=PopupTaskList.ClientID%>').scrollTop;
            console.log('Set -> PopupTaskList: x=' + xPos2 + ', y=' + yPos2);
        }
    }

    function EndRequestHandler(sender, args) {
        if ($get('<%=PopupTaskList.ClientID%>') != null) {
            console.log('Get -> PopupTaskList: x=' + xPos2 + ', y=' + yPos2);
            $get('<%=PopupTaskList.ClientID%>').scrollLeft = xPos2;
            $get('<%=PopupTaskList.ClientID%>').scrollTop = yPos2;
        }
    }

    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
</script>--%>
