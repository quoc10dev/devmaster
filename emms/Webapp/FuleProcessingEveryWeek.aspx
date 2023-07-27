<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FuleProcessingEveryWeek.aspx.cs"
    Inherits="FuleProcessingEveryWeek" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container-fluid">
        <div class="card bg-primary">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card-header">
                        <div class="card-text">
                            <h3 class="card-title">Fule processing</h3>
                        </div>
                        <div class="action">
                          <div class="btn btn-success btn-checkbox">
                              <asp:CheckBox ID="chkSelectImport" runat="server" OnCheckedChanged="chkSelectImport_CheckedChanged" AutoPostBack="true" Text="Import from Excel" />
                          </div>
                        </div>
                        <div class="filter">
                            <div class="form-inline" id="divFilter" runat="server">
                                <div class="filter-item">
                                    Từ ngày 
                                    <asp:TextBox ID="txtFilterTuNgay" runat="server" CssClass="form-input date" />
                                </div>
                                <div class="filter-item">
                                    đến ngày 
                                    <asp:TextBox ID="txtFilterDenNgay" runat="server" CssClass="form-input date" Enabled="false" />
                                </div>
                                <div class="filter-item">
                                    <asp:Button ID="btnPreviousDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnPrevious_Click" Text="<<" 
                                        Width="40px" ToolTip="Bấm để lùi lại 1 tuần" />
                                    <asp:Button ID="btnNextDate" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnNext_Click" Text=">>" 
                                        Width="40px" ToolTip="Bấm để tăng thêm 1 tuần"/>
                                </div>
                                <div class="w-100"></div>
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
                            <div class="form-inline" id="divUpload" runat="server" visible="false">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                    <ContentTemplate>
                                        <div class="filter-item">
                                            <asp:FileUpload ID="fileUpload" runat="server"/>
                                            <asp:Literal ID="ltrFileName" runat="server" />
                                        </div>
                                        <div class="filter-item">
                                            <asp:Button ID="btnImportFromExcel" runat="server" OnClick="btnImportFromExcel_Click"
                                                Text="Read data" CssClass="btn btn-info btn-sm"/>
                                            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Import" CssClass="btn btn-info btn-sm" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnImportFromExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                      <div class="table-responsive table-small" id="showGrid" runat="server">
                        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            OnRowDataBound="gridView_RowDataBound" ShowFooter="false" CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                            <HeaderStyle ForeColor="" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="40px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTT" runat="server" />
                                        <asp:HiddenField ID="hfIDTrangThietBi" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Nhóm xe</HeaderTemplate>
                                    <ItemTemplate>
                                         <%# Eval("TenNhom") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Loại xe</HeaderTemplate>
                                    <ItemTemplate>
                                         <%# Eval("TenLoai") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="90px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>Biển số</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBienSo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate1" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate2" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate2" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate2" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate3" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate3" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate3" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate4" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate4" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate4" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate5" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate5" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate5" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate6" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate6" runat="server" MaxLength="12" Style="width: 90px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate6" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="col"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDate7" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate7" runat="server" MaxLength="12" Style="width: 100px; text-align: center" class="form-input tiny weight"/>
                                        <asp:HiddenField ID="hfDate7" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Text="Save" />
                      </div>
                      <div class="table-responsive" id="showGridImport" runat="server">
                        <asp:GridView ID="gridExcelData" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" DataKeyNames=""
                            AllowSorting="true" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                            ShowFooter="false" CssClass="table table-striped table-hover"
                            UseAccessibleHeader="true" GridLines="None">
                        </asp:GridView>
                      </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
    
    <style>
        .btn-checkbox {
            padding-top: 7px;
            padding-bottom: 7px;
            text-transform: none;
        }
        .btn-checkbox input {
            transform: translate(0, 2px);
            margin-right: 5px;
        }
        .btn-checkbox label {
            font-size: 0.8rem;
            font-weight: bold;
        }
        .table-small thead th,
        .table-small tbody td {
            padding-left: 5px !important;
            padding-right: 5px !important;
        }
    </style>
    <script>
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


