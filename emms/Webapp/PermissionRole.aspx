<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PermissionRole.aspx.cs" Inherits="PermissionRole" %>

<%@ Register Src="~/UserControl/MyGridViewPager.ascx" TagPrefix="uc1" TagName="MyGridViewPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatePanelMasterPage" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <section class="container-fluid">
                <div class="card bg-primary">
                    <asp:UpdatePanel ID="upUserList" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card-header">
                                <div class="card-text">
                                    <h3 class="card-title">Permission Role</h3>
                                </div>
                                <div class="action">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info btn-sm" Text="Save" OnClick="btnUpdate_Click"></asp:Button>
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="OpenWindow()"
                                        Text="Back" UseSubmitBehavior="False" />
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="role-name">
                                    Role name:
                                  <asp:Label ID="lblRoleName" runat="server" Font-Bold="true" />
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">Equipment categories</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Company</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkCompany_View" runat="server" CssClass="form-check-input" />
                                                        View list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkCompany_Add" runat="server" CssClass="form-check-input" />
                                                        Add user</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkCompany_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit user</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkCompany_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete user</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Equipment group</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentGroup_View" runat="server" CssClass="form-check-input" />
                                                        View equipment group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentGroup_Add" runat="server" CssClass="form-check-input" />
                                                        Add equipment group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentGroup_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit equipment group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentGroup_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete equipment group</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Equipment type</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentType_View" runat="server" CssClass="form-check-input" />
                                                        View equipment type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentType_Add" runat="server" CssClass="form-check-input" />
                                                        Add equipment type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentType_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit equipment type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipmentType_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete equipment type</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Fuel quota</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuelQuota_View" runat="server" CssClass="form-check-input" />
                                                        View fuel quota</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuelQuota_Add" runat="server" CssClass="form-check-input" />
                                                        Add fuel quota</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuelQuota_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit fuel quota</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuelQuota_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete fuel quota</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Frequency working</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFrequencyWorking_View" runat="server" CssClass="form-check-input" />
                                                        View frequency working</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFrequencyWorking_Add" runat="server" CssClass="form-check-input" />
                                                        Add frequency working</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFrequencyWorking_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit frequency working</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFrequencyWorking_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete frequency working</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">Maintenance categories</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenace type</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkChildMaintenanceType_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkChildMaintenanceType_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkChildMaintenanceType_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkChildMaintenanceType_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance type</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance level</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceLevel_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance level</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceLevel_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance level</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceLevel_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance level</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceLevel_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance level</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance item group</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceGroup_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance item group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceGroup_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance item group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceGroup_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance item group</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceGroup_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance item group</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance item</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceList_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance item list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceList_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance item</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceList_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance item</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenaceList_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance item</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance request form</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequestForm_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance request form list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequestForm_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance request form</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequestForm_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance request form</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequestForm_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance request form</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">Equipment management</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Equipment list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipment_View" runat="server" CssClass="form-check-input" />
                                                        View equipment</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipment_Add" runat="server" CssClass="form-check-input" />
                                                        Add equipment</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipment_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit equipment</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkEquipment_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete equipment</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Operation processing</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkOperationProcessing_View" runat="server" CssClass="form-check-input" />
                                                        View operation processing
                                                    </label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkOperationProcessing_Add" runat="server" CssClass="form-check-input" />
                                                        Add operation processing</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkOperationProcessing_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit operation processing</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkOperationProcessing_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete operation processing</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Fule processing</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuleProcessing_View" runat="server" CssClass="form-check-input" />
                                                        View fule processing</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuleProcessing_Add" runat="server" CssClass="form-check-input" />
                                                        Add fule processing</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuleProcessing_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit fule processing</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFuleProcessing_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete fule processing</label>
                                                </p>
                                            </div>
                                        </div>
                                         <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Get data from GPS</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkTransferData_View" runat="server" CssClass="form-check-input" />
                                                        View data from GPS</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkTransferData_TransferData" runat="server" CssClass="form-check-input" />
                                                        Transfer data from GPS to EMMS</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">Maintenance & Repair management</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6" style="display: none!important">
                                            <div class="card">
                                                <h4 class="card-title">Failure type</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFailureType_View" runat="server" CssClass="form-check-input" />
                                                        View failure type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFailureType_Add" runat="server" CssClass="form-check-input" />
                                                        Add failure type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFailureType_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit failure type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkFailureType_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete failure type</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance request</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequest_View" runat="server" CssClass="form-check-input" />
                                                        View maintenance request list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequest_Add" runat="server" CssClass="form-check-input" />
                                                        Add maintenance request</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequest_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit maintenance request</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenanceRequest_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete maintenance request</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Repair request</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRepairRequest_View" runat="server" CssClass="form-check-input" />
                                                        View repair request list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRepairRequest_Add" runat="server" CssClass="form-check-input" />
                                                        Add repair request</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRepairRequest_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit repair request</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRepairRequest_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete repair request</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list" style="display: none!important">
                                    <h4 class="role-group">Warehouse management</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Provider list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkProviderList_View" runat="server" CssClass="form-check-input" />
                                                        View provider list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkProviderList_Add" runat="server" CssClass="form-check-input" />
                                                        Add provider</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkProviderList_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit provider</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkProviderList_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete provider</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Department list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkDepartmentList_View" runat="server" CssClass="form-check-input" />
                                                        View department list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkDepartmentList_Add" runat="server" CssClass="form-check-input" />
                                                        Add department</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkDepartmentList_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit department</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkDepartmentList_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete department</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Material type list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterialType_View" runat="server" CssClass="form-check-input" />
                                                        View material type list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterialType_Add" runat="server" CssClass="form-check-input" />
                                                        Add material type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterialType_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit material type</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterialType_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete material type</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Material list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterial_View" runat="server" CssClass="form-check-input" />
                                                        View material list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterial_Add" runat="server" CssClass="form-check-input" />
                                                        Add material</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterial_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit material</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaterial_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete material</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">Report management</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Warning registration</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewWarningRegistrationReport" runat="server" CssClass="form-check-input" />
                                                        View warning registration</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Warning maintenance</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkWarningMaintenance_View" runat="server" CssClass="form-check-input" />
                                                        View warning maintenance report</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Operation processing</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewOperationProcessingReport" runat="server" CssClass="form-check-input" />
                                                        View operation processing</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Fule processing</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewFuleProcessingReport" runat="server" CssClass="form-check-input" />
                                                        View fule processing</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Fuel consumption</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewFuelConsumption" runat="server" CssClass="form-check-input" />
                                                        View fuel consumption</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance plan</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkMaintenancePlan" runat="server" CssClass="form-check-input" />
                                                        View maintenance plan</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Activity history</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkActivityHistory" runat="server" CssClass="form-check-input" />
                                                        View activity history</label>
                                                </p>
                                            </div>
                                        </div>
                                         <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Maintenance request</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewMaintenanceRequestReport" runat="server" CssClass="form-check-input" />
                                                        View maintenance request</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Repair request</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkViewRepairRequestReport" runat="server" CssClass="form-check-input" />
                                                        View repair request</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="role-list">
                                    <h4 class="role-group">System management</h4>
                                    <div class="row permission">
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">User list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkUser_View" runat="server" CssClass="form-check-input" />
                                                        View list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkUser_Add" runat="server" CssClass="form-check-input" />
                                                        Add user</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkUser_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit user</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkUser_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete user</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Role list</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRole_View" runat="server" CssClass="form-check-input" />
                                                        View list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRole_Add" runat="server" CssClass="form-check-input" />
                                                        Add role</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRole_Edit" runat="server" CssClass="form-check-input" />
                                                        Edit role</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkRole_Delete" runat="server" CssClass="form-check-input" />
                                                        Delete role</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkPermission_View" runat="server" CssClass="form-check-input" />
                                                        View right list</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkPermission_Update" runat="server" CssClass="form-check-input" />
                                                        Update right for role</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Configuration</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkSetting_EquipmentManagement" runat="server" CssClass="form-check-input" />
                                                        Equipment management setting</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkSetting_MaintenanceManagement" runat="server" CssClass="form-check-input" />
                                                        Maintanance management setting</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6">
                                            <div class="card">
                                                <h4 class="card-title">Profile</h4>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkView_Profile" runat="server" CssClass="form-check-input" />
                                                        View Profile</label>
                                                </p>
                                                <p class="card-category">
                                                    <label class="form-check-label">
                                                        <asp:CheckBox ID="chkChange_Password" runat="server" CssClass="form-check-input" />
                                                        Change password</label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
            <script type="text/javascript">
                function OpenWindow() {
                    location.href = "RoleList.aspx";
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


