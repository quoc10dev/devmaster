<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairRequestForm.aspx.cs" Inherits="RepairRequestForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Repair Request</title>
    <link href="dist/styles.css" rel="stylesheet">

    <!-- Favicons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/icon.png">
    <link rel="shortcut icon" href="img/icon.png">
</head>
<body>
    <main class="compact bg-white">
        <section class="container a4paper">
            <div class="request">
                <div class="request-header">
                    <table class="table table-bordered" border="1" style="border-collapse: collapse">
                        <tr>
                            <td rowspan="4" class="request-logo" width="250">
                                <img src="img/logo.png" height="100">
                            </td>
                            <td rowspan="4" class="request-title">
                                <h3>PHIẾU SỬA CHỮA VÀ KHẮC PHỤC SỰ CỐ TRANG THIẾT BỊ<br>
                                    <i>Repair & Overcom problem Request</i></h3>
                            </td>
                            <td width="250">Mã số: SPM/BM/QLTTB/003</td>
                        </tr>
                        <tr>
                            <td>Lần: 05</td>
                        </tr>
                        <tr>
                            <td>Ngày ban hành: 05/03/2020</td>
                        </tr>
                        <tr>
                            <td>Trang: </td>
                        </tr>
                    </table>
                    <div class="request-id">
                        <p>
                            <i>Mã số thẻ:
                            <asp:Label ID="lblMaPhieu" runat="server" /></i>
                        </p>
                    </div>
                </div>

                <div class="request-body">
                    <div class="catalog">
                        <h3 class="name">A. TRANG THIẾT BỊ SỬA CHỮA/ REPAIRING EQUIPMENT</h3>
                        <table class="table table-bordered" border="1" style="border-collapse: collapse">
                            <tr>
                                <td width="50%">Tên thiết bị/ <i>Vehicle’s name</i>:
                                    <asp:Label ID="lblEquipmentName" runat="server" /></td>
                                <td width="50%">Biển kiểm soát số/ <i>License Plates</i>:
                                    <asp:Label ID="lblBienSo" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Số giờ hoặc số km/ <i>Hours or Kilometer:</i>
                                    <asp:Label ID="lblSoGioHoacSoKm" runat="server" /></td>
                                <td>
                                    <div class="plan">
                                        <p>Thời gian thực hiện:</p>
                                        <ol>
                                            <li>Thời gian vào xưởng: ……………………………………</li>
                                            <li>Thời gian xuất xưởng: ……………………………………</li>
                                        </ol>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="plan">
                                        <p>Người thực hiện/ <i>Checked by</i>:</p>
                                        <ol>
                                            <li>…………………………………………………………… Ký/Signature……………………………………………………</li>
                                            <li>…………………………………………………………… Ký/Signature……………………………………………………</li>
                                            <li>…………………………………………………………… Ký/Signature……………………………………………………</li>
                                        </ol>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="catalog">
                        <h3 class="name">B. NỘI DUNG SỬA CHỮA/ REPAIR CONTENTS</h3>
                        <table class="table table-bordered" border="1" style="border-collapse: collapse">
                            <tr class="center middle">
                                <td width="50">TT/No.</td>
                                <td>NỘI DUNG HẠNG MỤC SỬA CHỮA<br>
                                    WORKING CONTENT</td>
                                <td>PHƯƠNG ÁN SỬA CHỮA<br>
                                    SOLUTION</td>
                                <td width="150">GHI CHÚ<br>
                                    REMARK</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">1</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">2</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">3</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">4</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">5</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">6</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">7</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                    </div>
                    <div class="page-break"></div>
                    <div class="catalog">
                        <h3 class="name">C. HẠNG MỤC VẬT TƯ PHÁT SINH/ MATERRIAL ARISING SECTION</h3>
                        <table class="table table-bordered" border="1" style="border-collapse: collapse">
                            <tr class="center middle">
                                <td width="50">TT/No.</td>
                                <td>TÊN/ NAME (Part number)</td>
                                <td width="100">ĐVT/UNIT</td>
                                <td width="100">SL/Qty.</td>
                                <td width="150">GHI CHÚ/ COMMENTS</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">1</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">2</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">3</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">4</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">5</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">6</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 90px; vertical-align: middle">7</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>

                    <div class="catalog">
                        <h3 class="name">D. NHẬN XÉT – ĐÁNH GIÁ CỦA CÁN BỘ ĐỘI SỬA CHỮA/ COMMENT – APPRECIATE BY SUPERVISOR</h3>
                        <div class="line">
                            <span>Nhận xét:</span><p></p>
                        </div>
                        <p>
                            Đạt yêu cầu kiểm tra kỹ thuật, bàn giao đưa thiết bị vào khai thác (OK):
                            <check class="checkbox"><span>&#9633;</span></check>
                            - Không đạt (NG):
                            <check class="checkbox"><span>&#9633;</span></check>
                        </p>
                        <div class="signature">
                            <p>
                                TRƯỞNG BỘ PHẬN KỸ THUẬT<br>
                                TECHNICAL MANAGER
                            </p>
                        </div>
                    </div>
                    <div style="height: 80px"></div>
                    <div class="catalog">
                        <h3 class="name">E. BÀN GIAO TRANG THIẾT BỊ VÀO KHAI THÁC/ HANDOVER EQUIPMENT REQUEST</h3>
                        <p>Bàn giao TTB vào lúc ......h, ngày ...... tháng ....... năm ........</p>
                        <div class="signature">
                            <p>
                                ĐỘI XE SÂN ĐỖ<br>
                                RAMP DRIVER TEAM LEADER
                            </p>
                        </div>
                    </div>

                </div>

                <div class="request-footer">
                    <div align="left">
                        <%--<b>Nơi nhận:</b>
                        <ul>
                            <li>Phòng DVSD</li>
                            <li>Lưu tại đội QLTTB</li>
                        </ul>--%>
                    </div>
                </div>

            </div>
        </section>
    </main>

    <style>
    body {
        width: 280mm;
    }

    @media all {
        .page-break {
            display: none;
        }
    }

    @media print {
        .page-break {
            display: block;
            page-break-before: always;
        }
    }
</style>
</body>
</html>
