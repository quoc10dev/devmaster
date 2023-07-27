<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportFuleProcessing.aspx.cs"
    Inherits="ReportFuleProcessing" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Fule processing report</h3>
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

                    <div class="card-body table-responsive" id="showGrid" runat="server">
                        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            ShowFooter="false" CssClass="table table-striped table-hover"
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
    </script>
</asp:Content>


