<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <header>
        <nav class="navbar navbar-light bg-white">
            <div class="container">
                <button class="onoffcanvas-toggler is-animated" data-target="#sidebar" data-toggle="onoffcanvas" aria-expanded="false"></button>
                <a class="navbar-brand mx-auto" href="#">
                    <img src="img/logo.png">
                    <span class="nav-title">Admin</span>
                </a>
            </div>
        </nav>
    </header>
    <section class="container-fluid">
        <div class="row card-stats">
            <div class="col-lg">
                <span style="color: #7b1fa2; font-size: 25px; font-weight: bold">
                    <asp:Literal ID="ltTitle" runat="server"></asp:Literal></span>
            </div>
            <div class="col-lg">
            </div>
        </div>
    </section>

    <section class="container-fluid" style="display: none">
        <div class="row card-stats">
            <div class="col-lg-3 col-sm-6">
                <div class="card bg-warning">
                    <div class="card-header">
                        <div class="card-icon">
                            <i class="material-icons">content_copy</i>
                        </div>
                        <p class="card-category">Used Space</p>
                        <h3 class="card-title">49/50
                <small>GB</small>
                        </h3>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <i class="material-icons text-danger">warning</i>
                            <a href="#">Get More Space...</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-sm-6">
                <div class="card bg-success">
                    <div class="card-header">
                        <div class="card-icon">
                            <i class="material-icons">store</i>
                        </div>
                        <p class="card-category">Revenue</p>
                        <h3 class="card-title">$34,245</h3>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <i class="material-icons">date_range</i> Last 24 Hours
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-sm-6">
                <div class="card bg-danger">
                    <div class="card-header">
                        <div class="card-icon">
                            <i class="material-icons">info_outline</i>
                        </div>
                        <p class="card-category">Fixed Issues</p>
                        <h3 class="card-title">75</h3>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <i class="material-icons">local_offer</i> Tracked from Github
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-sm-6">
                <div class="card bg-info">
                    <div class="card-header">
                        <div class="card-icon">
                            <i class="fab fa-twitter"></i>
                        </div>
                        <p class="card-category">Followers</p>
                        <h3 class="card-title">+245</h3>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <i class="material-icons">update</i> Just Updated
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="container-fluid">
        <div class="card-info">
            <div class="card bg-secondary">
                <div class="card-header">
                    <div class="card-text">
                        Danh mục trang thiết bị
                    </div>
                </div>
                <div class="card-body" style="font-weight: normal !important">
                    <div class="card-category">+ Company&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Công ty</div>
                    <div class="card-category">+ Equipment group &nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Nhóm trang thiết bị</div>
                    <div class="card-category">+ Equipment type&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Loại trang thiết bị</div>
                    <div class="card-category">+ Fuel quota&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Định mức nhiên liệu</div>
                    <div class="card-category">+ Frequency working&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Tần suất hoạt động</div>
                </div>
            </div>
            <div class="card bg-secondary">
                <div class="card-header">
                    <div class="card-text">
                        Danh mục bảo dưỡng - sửa chữa
                    </div>
                </div>
                <div class="card-body" style="font-weight: normal !important">
                   <div class="card-category" style="display: none">+ Failure type&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Loại hư hỏng thiết bị</div>
                    <div class="card-category">+ Maintenance type&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Loại bảo dưỡng</div>
                    <div class="card-category">+ Maintenance level&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Cấp bảo dưỡng</div>
                    <div class="card-category">+ Maintenance item group&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Nhóm hạng mục bảo dưỡng</div>
                    <div class="card-category">+ Maintenance item&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Hạng mục bảo dưỡng</div>
                    <div class="card-category">+ Maintenance request form&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Mẫu in thẻ bảo dưỡng</div>
                </div>
            </div>
            <div class="card bg-secondary">
                <div class="card-header">
                    <div class="card-text">
                        Quản lý trang thiết bị
                    </div>
                </div>
                <div class="card-body" style="font-weight: normal !important">
                    <div class="card-category">+ Equipment list&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Danh sách trang thiết bị</div>
                    <div class="card-category">+ Operation processing&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Quá trình hoạt động trang thiết bị</div>
                    <div class="card-category">+ Fule processing&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Quá trình nạp nhiên liệu</div>
                    <div class="card-category">+ Get data from GPS&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Lấy dữ liệu từ GPS</div>
                </div>
            </div>
            <div class="card bg-secondary">
                <div class="card-header">
                    <div class="card-text">
                        Quản lý bảo dưỡng - sửa chữa
                    </div>
                </div>
                <div class="card-body" style="font-weight: normal !important">
                    <div class="card-category">+ Maintenance request&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Thẻ bảo dưỡng</div>
                    <div class="card-category">+ Repair request&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Phiếu sửa chữa</div>
                </div>
            </div>
            <div class="card bg-secondary">
                <div class="card-header">
                    <div class="card-text">
                        Quản lý báo cáo
                    </div>
                </div>
                <div class="card-body" style="font-weight: normal !important">
                    <div class="card-category">+ Warning registration&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Cảnh báo ngày đăng kiểm kế tiếp</div>
                    <div class="card-category">+ Warning maintenance&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Cảnh bảo thời hạn bảo dưỡng</div>
                    <div class="card-category">+ Operation processing&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Quá trình hoạt động trang thiết bị</div>
                    <div class="card-category">+ Fule processing&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Quá trình nạp nhiên liệu</div>
                    <div class="card-category">+ Fuel consumption&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Định mức tiêu hao nhiên liệu</div>
                    <div class="card-category">+ Maintenance plan&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Kế hoạch bảo dưỡng</div>
                    <div class="card-category">+ Maintenance request&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Lịch sử bảo dưỡng</div>
                    <div class="card-category">+ Repair request&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;Lịch sử sửa chữa</div>
                </div>
            </div>
        </div>
    </section>

    <!--section class="container-fluid">
      <div class="row card-info">
        <div class="col-lg">
          <div class="card bg-secondary">
            <div class="card-header">
              <div class="card-text">
                <h3 class="card-title">Calendar</h3>
              </div>
              <div class="card-category">Jobs, events in April 2018</div>
            </div>
            <div class="card-body">
              <div class="calendar"></div>
            </div>
          </div>
        </div>
        <div class="col-lg">
          <div class="card">
            <div class="card-header">
              <div class="card-text">
                <h3 class="card-title">Customer</h3>
              </div>
              <div class="card-category">New customers on 27th April, 2018</div>
            </div>
            <div class="card-body table-responsive">
              <table class="table table-hover">
                <thead class="text-warning">
                  <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Phone</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>1</td>
                    <td>Dakota Rice</td>
                    <td>0936738234</td>
                  </tr>
                  <tr>
                    <td>2</td>
                    <td>Minerva Hooper</td>
                    <td>0923789536</td>
                  </tr>
                  <tr>
                    <td>3</td>
                    <td>Sage Rodriguez</td>
                    <td>01256142453</td>
                  </tr>
                  <tr>
                    <td>4</td>
                    <td>Philip Chaney</td>
                    <td>0938735764</td>
                  </tr>
                  <tr>
                    <td>5</td>
                    <td>Minerva Hooper</td>
                    <td>01238433789</td>
                  </tr>
                  <tr>
                    <td>6</td>
                    <td>Sage Rodriguez</td>
                    <td>01256142488</td>
                  </tr>
                  <tr>
                    <td>7</td>
                    <td>Philip Chaney</td>
                    <td>0938735646</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </section-->

</asp:Content>

