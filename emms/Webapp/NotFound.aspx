<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotFound.aspx.cs" Inherits="NotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>404 Not Found | EMMS</title>
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
        <main class="fullscreen">
            <div class="error">
                <h1><i class="material-icons">pages</i> 404</h1>
                <h2>The page you requested could not be found.</h2>
                <div class="mt-4">
                  <a href="/Default.aspx" class="btn btn-primary">Back to home page</a>
                </div>
            </div>
        </main>
    </form>

    <!-- jQuery -->
    <script src="//code.jquery.com/jquery-3.3.1.js"></script>
    <script>window.jQuery || document.write('<script src="ext/jquery/jquery.min.js"><\/script>')</script>

    <!-- Scripts -->
    <script src="dist/bundle.js"></script>
</body>
</html>
