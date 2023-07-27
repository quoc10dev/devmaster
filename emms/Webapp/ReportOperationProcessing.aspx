<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportOperationProcessing.aspx.cs"
    Inherits="ReportOperationProcessing" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .selectedCell {
            background-color: aqua !important;
        }

        .unselectedCell {
            background-color: white !important;
        }

        /*
        td:hover {
            color: #fff;
            background: #CA293E;
        }
        */

        td:focus {
            background: #f44;
        }
    </style>

    <!-- jQuery -->
    <script src="//code.jquery.com/jquery-3.3.1.js"></script>
    <script>window.jQuery || document.write('<script src="ext/jquery/jquery.min.js"><\/script>')</script>

    <script type="text/javascript">
        window.onload = function () {

            var currCell = $('table').find('tbody').find('td').first();
            var editing = false;

            // User clicks on a cell
            $('table').find('tbody').find('td').click(function () {
                currCell = $(this);
                //alert(currCell);
            });

            // User navigates table using keyboard
            $('table').find('tbody').keydown(function (e) {
                //alert('right');
                var c = "";
                if (e.which == 39) {
                    // Right Arrow
                    //alert('right');
                    c = currCell.next();
                }
                else if (e.which == 37) {
                    // Left Arrow
                    c = currCell.prev();
                }
                else if (e.which == 38) {
                    // Up Arrow
                    c = currCell.closest('tr').prev().find('td:eq(' + currCell.index() + ')');
                }
                else if (e.which == 40) {
                    // Down Arrow
                    c = currCell.closest('tr').next().find('td:eq(' + currCell.index() + ')');
                }
                else if (!editing && (e.which == 13 || e.which == 32)) {
                    // Enter or Spacebar - edit cell
                    e.preventDefault();
                }
                else if (!editing && (e.which == 9 && !e.shiftKey)) {
                    // Tab
                    e.preventDefault();
                    c = currCell.next();
                }
                else if (!editing && (e.which == 9 && e.shiftKey)) {
                    // Shift + Tab
                    e.preventDefault();
                    c = currCell.prev();
                }

                // If we didn't hit a boundary, update the current cell
                if (c.length > 0) {
                    currCell = c;
                    currCell.focus();
                }
            });

            // User can cancel edit by pressing escape
            $('#edit').keydown(function (e) {
                if (editing && e.which == 27) {
                    editing = false;
                    $('#edit').hide();
                    currCell.toggleClass("editing");
                    currCell.focus();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Operation processing report</h3>
                        </div>
                        <div class="filter" style="margin-top: 20px">
                            <div class="form-inline" id="divFilter" runat="server">
                                <div class="filter-item">
                                    Từ ngày 
                                    <asp:TextBox ID="txtFilterTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtFilterDenNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnPreviousDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrevious_Click" Text="<<"
                                        Width="40px" ToolTip="Bấm để lùi lại 1 tháng" />
                                    <asp:Button ID="btnNextDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnNext_Click" Text=">>"
                                        Width="40px" ToolTip="Bấm để tăng thêm 1 tháng" />
                                </div>
                            </div>
                            <div class="form-inline" id="div1" runat="server">
                                <div class="filter-item">
                                    Công ty
                                    <asp:DropDownList ID="dlFilterCompany" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Nhóm xe
                                    <asp:DropDownList ID="dlFilterNhomTrangThietBi" runat="server" CssClass="form-input"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlFilterNhomTrangThietBi_SelectedIndexChanged" />
                                </div>
                                <div class="filter-item">
                                    Loại xe
                                    <asp:DropDownList ID="dlFilterLoaiTrangThietBi" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    Biển số
                                    <asp:TextBox ID="txtFilterBienSo" runat="server" CssClass="form-input" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnFilter_Click" Text="Filter" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnClear_Click" Text="Clear" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card-body table-responsive" id="showGrid" runat="server" style="overflow: scroll;">
                        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            ShowFooter="false"
                            CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

    <script type="text/javascript">

        function DateControl(el) {
            var dateFormat = 'DD/MM/YYYY';
            var dateMask = new IMask(el, {
                mask: Date,
                pattern: dateFormat,
                lazy: false,

                format: function (date) {
                    return moment(date).format(dateFormat);
                },
                parse: function (str) {
                    return moment(str, dateFormat);
                },

                blocks: {
                    YYYY: {
                        mask: IMask.MaskedRange,
                        from: 1900,
                        to: 9999
                    },
                    MM: {
                        mask: IMask.MaskedRange,
                        from: 1,
                        to: 12,
                        maxLength: 2
                    },
                    DD: {
                        mask: IMask.MaskedRange,
                        from: 1,
                        to: 31,
                        maxLength: 2
                    }
                }
            });
        }

        //window.onload = function () {
        //    var $grid = $('#ContentPlaceHolder1_gridView');

        //    //Highlight the row when mouse over on it  
        //    //$grid.find('tr').hover(function () {
        //    //    $(this).addClass('selectedCell');
        //    //}, function () { $(this).removeClass('selectedCell'); });

        //    // Highlight the cell when mouse over on it
        //    $grid.find('td').hover(function () {
        //        $(this).addClass('selectedCell');
        //    }, function () { $(this).removeClass('selectedCell'); });

        //    //Highlight the row when the row clicked
        //    // $grid.find('tr').click(function () {
        //    //    $grid.find('tr').removeClass('selectedCell');
        //    //    $(this).addClass('selectedCell');
        //    //});

        //    //Highlight the cell when the row clicked
        //     $grid.find('td').click(function () {
        //        $grid.find('td').removeClass('selectedCell');
        //        $(this).addClass('selectedCell');
        //    });
        //}

        //window.onload = function () {

        //    var currCell = $('td').first();
        //    var editing = false;

        //    // User clicks on a cell
        //    $('td').click(function () {
        //        currCell = $(this);
        //    });

        //    // User navigates table using keyboard
        //    $('table').keydown(function (e) {
        //        var c = "";
        //        if (e.which == 39) {
        //            // Right Arrow
        //            c = currCell.next();
        //        }
        //        else if (e.which == 37) {
        //            // Left Arrow
        //            c = currCell.prev();
        //        }
        //        else if (e.which == 38) {
        //            // Up Arrow
        //            c = currCell.closest('tr').prev().find('td:eq(' + currCell.index() + ')');
        //        }
        //        else if (e.which == 40) {
        //            // Down Arrow
        //            c = currCell.closest('tr').next().find('td:eq(' + currCell.index() + ')');
        //        }
        //        else if (!editing && (e.which == 13 || e.which == 32)) {
        //            // Enter or Spacebar - edit cell
        //            e.preventDefault();
        //        }
        //        else if (!editing && (e.which == 9 && !e.shiftKey)) {
        //            // Tab
        //            e.preventDefault();
        //            c = currCell.next();
        //        }
        //        else if (!editing && (e.which == 9 && e.shiftKey)) {
        //            // Shift + Tab
        //            e.preventDefault();
        //            c = currCell.prev();
        //        }

        //        // If we didn't hit a boundary, update the current cell
        //        if (c.length > 0) {
        //            currCell = c;
        //            currCell.focus();
        //        }
        //    });
        //}

        //hight light row in gridview
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {
            UpperBound = parseInt('<%= this.gridView.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;
        }

        function SelectRow(CurrentRow, RowIndex) {
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound) return;

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#e1bee7';
                CurrentRow.style.color = '';
            }

            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }

        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;

            if (KeyCode == 40)
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            else if (KeyCode == 38)
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);

            return false;
        }

    </script>
</asp:Content>


