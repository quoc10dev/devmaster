<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Access denied | EMMS</title>
    <meta name="description" content="">
    <meta name="keywords" content="">
    <meta name="author" content="IT team - AGS">
    <meta name="copyright" content="IT team">
    <meta name="robots" content="noindex">

    <!-- Styles -->
    <link href="dist/styles.css" rel="stylesheet">

    <!-- Favicons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/icon.png">
    <link rel="shortcut icon" href="img/icon.png">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <main class="fullscreen">
                <div class="card lock">
                    <div class="card-header">
                      <div class="card-avatar">
                        <div class="avatar">
                          <img src="img/avatar.jpg" alt="Access denied">
                        </div>
                        <h2 class="pb-0">Access denied!</h2>
                      </div>
                    </div>
                    <div class="card-body">
                      <div class="mb-2">You are not authorized to access this page.</div>
                      <div class="mt-3">
                        <a href="/Default.aspx" class="btn btn-outline-primary">Back to home page</a>
                      </div>
                    </div>
                </div>
            </main>

            <!-- jQuery -->
            <script src="//code.jquery.com/jquery-3.3.1.js"></script>
            <script>window.jQuery || document.write('<script src="ext/jquery/jquery.min.js"><\/script>')</script>

            <!-- Scripts -->
            <script src="dist/bundle.js"></script>
        </div>
    </form>
</body>
</html>
