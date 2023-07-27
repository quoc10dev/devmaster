<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestForm.aspx.cs" Inherits="RequestForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maintenance Request</title>
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
                    <table class="table table-bordered" border="1" style="border-collapse: collapse;">
                        <tr>
                            <td rowspan="4" class="request-logo" width="250">
                                <img src="img/logo.png" height="100">
                            </td>
                            <td rowspan="4" class="request-title">
                                <h3>THẺ BẢO DƯỠNG<br>
                                    <i>Maintenance Request</i></h3>
                            </td>
                            <td width="250">Mã số: SPM/BM/QLTTB/002</td>
                        </tr>
                        <tr>
                            <td>Lần: 07</td>
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
                            <asp:Label ID="lblMaThe" runat="server" />
                            </i>
                        </p>
                    </div>
                    <div class="request-body">
                        <div class="catalog">
                            <h3 class="name">A. TRANG THIẾT BỊ BẢO DƯỠNG/ EQUIPMENT MAINTENANCE</h3>
                            <table class="table table-bordered" border="1" style="border-collapse: collapse">
                                <tr>
                                    <td width="50%">Tên thiết bị/ <i>Vehicle’s name</i>:
                                        <asp:Label ID="lblEquipmentName" runat="server" /></td>
                                    <td width="50%">Biển kiểm soát số/ <i>License Plates</i>:
                                        <asp:Label ID="lblBienSo" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Số giờ hoặc số km/ <i>Hours or Kilometer:
                                        <asp:Label ID="lblSoGioHoacSoKm" runat="server" />
                                        <asp:Label ID="lblNgayBaoDuong" runat="server" Visible="false" />
                                    </i></td>
                                    <td>
                                        <div class="type">
                                            Loại bảo dưỡng/ <i>Types of Maintence:</i><br>
                                            <p class="checkbox">
                                                <asp:Literal ID="ltrLevel1" runat="server" />
                                                <asp:Label runat="server" ID="lblLevel1_Position1" Style="margin-left:0; font-size: 18px"/>
                                                <asp:Literal ID="ltrLevel2" runat="server" />
                                                <asp:Label runat="server" ID="lblLevel2_Position1" Style="margin-left:0; font-size: 18px"/>
                                                <asp:Literal ID="ltrLevel3" runat="server" />
                                                <asp:Label runat="server" ID="lblLevel3_Position1" Style="margin-left:0; font-size: 18px"/>
                                                <asp:Literal ID="ltrLevel4" runat="server" />
                                                <asp:Label runat="server" ID="lblLevel4_Position1" Style="margin-left:0; font-size: 18px"/>
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="plan">
                                            <p>Người thực hiện/ <i>Checked by</i>:</p>
                                            <ol>
                                                <li>………………………… Ký/Signature…………………</li>
                                                <li>………………………… Ký/Signature…………………</li>
                                                <li>………………………… Ký/Signature…………………</li>
                                            </ol>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="plan">
                                            <p>Thời gian thực hiện:</p>
                                            <ol>
                                                <li>Thời gian vào xưởng: …………………………………</li>
                                                <li>Thời gian xuất xưởng: …………………………………</li>
                                            </ol>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="catalog">
                        <h3 class="name">B. NỘI DUNG BẢO DƯỠNG/ EQUIPMENT MAINTENANCE CONTENT <i>(Kèm theo)</i></h3>
                    </div>

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
                                <td class="center" style="height: 70px; vertical-align: middle">1</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="center" style="height: 70px; vertical-align: middle">2</td>
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

                    <div class="catalog" style="margin-top: 75px">
                        <h3 class="name">E. BÀN GIAO TRANG THIẾT BỊ VÀO KHAI THÁC/ HANDOVER EQUIPMENT REQUEST</h3>
                        <p>Bàn giao TTB vào lúc ......h, ngày ...... tháng ....... năm ........</p>
                        <div class="signature">
                            <p>
                                ĐỘI XE SÂN ĐỖ<br>
                                RAMP DRIVER TEAM LEADER
                            </p>
                        </div>
                    </div>

                    <div class="page-break"></div>
                    <h3 class="center" style="margin-top: 50px; font-style:italic"><asp:Label ID="lblBaoDuongTheo" runat="server"></asp:Label></h3>
                    <h4 class="center" style="margin-top: 0px"><b>NỘI DUNG BẢO DƯỠNG/ EQUIPMENT MAINTENANCE CONTENT<b /></h4>
                    <table class="table table-bordered" border="1" style="border-collapse: collapse">
                        <tr class="center middle">
                            <td width="50" rowspan="2">TT<br>
                                NO</td>
                            <td rowspan="2">NỘI DUNG CÔNG VIỆC THỰC HIỆN<br>
                                WORKING CONTENTS</td>
                            <td colspan="8">LOẠI BẢO DƯỠNG<br>
                                TYPE OF MAINTENANCE</td>
                            <td width="150" rowspan="2">GHI CHÚ<br>
                                REMARK</td>
                        </tr>
                        <tr class="center middle">
                            <td colspan="2">
                                <asp:Label ID="lblLevel1_Position2" runat="server" /></td>
                            <td colspan="2">
                                <asp:Label ID="lblLevel2_Position2" runat="server" /></td>
                            <td colspan="2">
                                <asp:Label ID="lblLevel3_Position2" runat="server" /></td>
                            <td colspan="2">
                                <asp:Label ID="lblLevel4_Position2" runat="server" /></td>
                        </tr>

                        <asp:Literal ID="ltrContent" runat="server" />

                        <tr>
                            <td colspan="11" class="note">
                                <ul>
                                    <li><b><u>Giải thích:</u></b>
                                        <ul>
                                            <li>I (Inspect): Kiểm tra nếu phát hiện hư hỏng thì sửa chữa hoặc thay thế;</li>
                                            <li>I* ( Release for inspection): Tháo, kiểm tra nếu phát hiện hư hỏng thì sửa chữa hoặc thay thế;</li>
                                            <li>D (Drain): Xả cặn;</li>
                                            <li>T (Retighten): Siết chặt;</li>
                                            <li>R (Replace): Thay thế;</li>
                                            <li>L (Lubricate): Tra dầu mỡ, bôi trơn;</li>
                                            <li>C (Clean): Kiểm tra, làm sạch;</li>
                                            <li>M (Measure): Đo đạc;</li>
                                            <li>A (Adjust): Hiệu chỉnh;</li>
                                        </ul>
                                    </li>
                                    <li><b><u>Ghi nhận kết quả kiểm tra:</u></b>
                                        <ul>
                                            <li>Đánh dấu “√” cho các công việc ĐẠT;</li>
                                            <li>Đánh dấu “X” cho các công việc KHÔNG ĐẠT;</li>
                                            <li>Tất cả các công việc được thực hiện sau khi kiểm tra và hiệu chỉnh nếu không đạt sẽ được ghi vào phần
                              HẠNG MỤC PHÁT
                              SINH (all checked parameter after adjustment will be recorded into ARISING section);</li>
                                            <li>Biểu mẫu có thể thay đổi các nội dung KT theo từng TTB nhưng phải giữ nguyên cấu trúc;</li>
                                            <li>Chu kỳ BD lặp lại theo các thời gian BD trên.</li>
                                        </ul>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                </div>

               <div class="request-footer">
                    <div align="left">
                       <%-- <b>Nơi nhận:</b>
                        <ul style="font-weight:normal">
                            <li>Phòng DVSD</li>
                            <li>Lưu tại đội QLTTB</li>
                        </ul>--%>
                    </div>
                </div>

                <asp:Literal ID="ltrTaskNameList" runat="server" Visible="false" />
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
