<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyGridViewPager.ascx.cs" Inherits="UserControl_MyGridViewPager" %>

<div class="card-footer">
    <nav aria-label="Page navigation">
        <ul class="pagination">
            <li class="total">
                <p>
                    Total
                  <b>
                      <asp:Label ID="lbTotalRow" runat="server" Font-Bold="true" /></b> items in <b>
                          <asp:Label ID="lblTotalPages" runat="server" Font-Bold="true" /></b> pages
                </p>
            </li>
            <asp:Repeater ID="rptPager" runat="server" OnItemCreated="rptPager_ItemCreated">
                <ItemTemplate>
                    <li id="liPager" runat="server">
                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                        </asp:LinkButton>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <li class="sizer" id="liPageSize" runat="server">Page size: &nbsp
                <asp:DropDownList ID="drPageSize" runat="server" Width="70px"
                    OnSelectedIndexChanged="drPageSize_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="10" />
                    <asp:ListItem Value="20" />
                    <asp:ListItem Value="50" />
                    <asp:ListItem Value="100" />
                </asp:DropDownList>
            </li>
            <li class="jumper"></li>
        </ul>
    </nav>
</div>
