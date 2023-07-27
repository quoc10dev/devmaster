<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowMessage.ascx.cs" Inherits="UserControl_ShowMessage" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="modal fade" id="modalMessage" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="width: 400px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            <asp:Label ID="lblMessageType" runat="server"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body" style="font-weight:normal !important">
                        <asp:Literal ID="ltrContentMessage" runat="server"></asp:Literal>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function OpenMessage() {
        $('#modalMessage').modal('show')
    }
</script>
