<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectAnEquipment.ascx.cs" Inherits="UserControl_SelectAnEquipment" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="modal fade" id="modalSelectEquipmentDialog" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Equipment list</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvList1" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" ShowFooter="false" CssClass="table table-striped table-hover"
                            OnRowCreated="gvList1_RowCreated" 
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>ID</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ID") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="200px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Tên xe</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Ten") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("BienSo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Mã tài sản</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("MaTaiSan") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Số khung</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SoKhung") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại máy</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LoaiMay") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col" HeaderStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Số máy</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SoMay") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" OnClick="btnSelect_Click" Text="Save" OnClientClick="ClosePopup()" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div>
                        <asp:Label ID="selectedValue" runat="server" />
                        <asp:HiddenField ID="hfCurrentRowIndex" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfParentContainer" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfColumnValue" runat="server"></asp:HiddenField>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    var SelectedRow = null;
    var SelectedRowIndex = null;
    var UpperBound = null;
    var LowerBound = null;

    window.onload = function () {
        UpperBound = parseInt('<%= this.gvList1.Rows.Count %>') - 1;
        LowerBound = 0;
        SelectedRowIndex = -1;
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

    $(function () {
        RetainSelectedRow1();
    });

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

    function GetValueOfCell(rowindex) {
        var i, CellValue, Row;
        i = parseInt(rowindex) + 1;
        var table = document.getElementById('<%=gvList1.ClientID %>');
        Row = table.rows[i].cells[0];
        CellValue = Row.innerHTML;
        Row.children[0].hidden = true;
        alert('xin chao');
    }

    function ClosePopup() {
        $('#modalSelectEquipmentDialog').modal('hide')
    }
</script>
