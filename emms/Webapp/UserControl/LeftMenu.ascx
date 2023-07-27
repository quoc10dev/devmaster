<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="UserControl_LeftMenu" %>

<aside id="sidebar" class="onoffcanvas is-fixed is-left is-open">
    <h2 class="side-header">
        <a class="logo" href="Default.aspx">
            <img src="img/icon.png">
            <span>EMMS</span>
        </a>
        <button class="toggler">
            <i class="material-icons">format_align_center</i>
        </button>
    </h2>
    <div class="side-divider"></div>
    <div class="side-content">
        <nav class="side-user">
            <ul id="usermenu" class="metismenu">
                <li id="liCurrentUserInfo" runat="server">
                    <a href="#" class="has-arrow" aria-expanded="false" id="fullName" runat="server">
                        <span class="has-icon">
                            <i class="material-icons">account_circle</i>
                        </span>
                        <span class="nav-title">
                            <asp:Label ID="lblFullName" runat="server" /></span>
                    </a>
                    <ul aria-expanded="false" class="collapse" id="ulCurrentUser" runat="server">
                        <li id="liProfile" runat="server">
                            <a href="ViewProfile.aspx">
                                <span class="has-icon">
                                    <i class="text-icons">Pr</i>
                                </span>
                                <span class="nav-title">Profile</span>
                            </a>
                        </li>
                        <li id="liChangePassword" runat="server">
                            <a href="ChangePassword.aspx">
                                <span class="has-icon">
                                    <i class="text-icons">Pa</i>
                                </span>
                                <span class="nav-title">Change password</span>
                            </a>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">
                                <span class="has-icon">
                                    <i class="text-icons">Lo</i>
                                </span>
                                <span class="nav-title">Logout</span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </nav>
        <div class="side-divider"></div>
        <nav class="side-nav">
            <ul id="metismenu" class="metismenu">
                <li id="liDashboard" runat="server">
                    <a href="Default.aspx">
                        <span class="has-icon">
                            <i class="material-icons">dashboard</i>
                        </span>
                        <span class="nav-title">Dashboard</span>
                    </a>
                </li>
                <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
            </ul>
        </nav>
    </div>
</aside>

