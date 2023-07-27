<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>
<style type="text/css">
    * {
        font-size: 12px;
        font-family: 'Helvetica', Arial, Sans-Serif;
        box-sizing: border-box;
    }

    table, th, td {
        border-collapse: collapse;
        border: solid 1px #ccc;
        padding: 10px 20px;
        text-align: center;
    }

    th {
        background: #0f4871;
        color: #fff;
    }

    tr:nth-child(2n) {
        background: #f1f1f1;
    }

    td:hover {
        color: #fff;
        background: #CA293E;
    }

    td:focus {
        background: #f44;
    }

    .editing {
        border: 2px dotted #c9c9c9;
    }

    #edit {
        display: none;
    }
</style>


<script src="//code.jquery.com/jquery-3.3.1.js"></script>
<script>window.jQuery || document.write('<script src="ext/jquery/jquery.min.js"><\/script>')</script>

<script type="text/javascript">
    window.onload = function () {

        var currCell = $('td').first();
        var editing = false;

        // User clicks on a cell
        $('td').click(function () {
            currCell = $(this);
            //edit();
        });

        // Show edit box
        //function edit() {
        //    editing = true;
        //    currCell.toggleClass("editing");
        //    $('#edit').show();
        //    $('#edit #text').val(currCell.html());
        //    $('#edit #text').select();
        //}

        // User saves edits
        //$('#edit form').submit(function (e) {
        //    editing = false;
        //    e.preventDefault();
        //    // Ajax to update value in database
        //    $.get('#', '', function () {
        //        $('#edit').hide();
        //        currCell.toggleClass("editing");
        //        currCell.html($('#edit #text').val());
        //        currCell.focus();
        //    });
        //});

        // User navigates table using keyboard
        $('table').keydown(function (e) {
            var c = "";
            if (e.which == 39)
            {
                // Right Arrow
                c = currCell.next();
            }
            else if (e.which == 37)
            {
                // Left Arrow
                c = currCell.prev();
            }
            else if (e.which == 38)
            {
                // Up Arrow
                c = currCell.closest('tr').prev().find('td:eq(' + currCell.index() + ')');
            }
            else if (e.which == 40)
            {
                // Down Arrow
                c = currCell.closest('tr').next().find('td:eq(' + currCell.index() + ')');
            }
            else if (!editing && (e.which == 13 || e.which == 32))
            {
                // Enter or Spacebar - edit cell
                e.preventDefault();
                //edit();
            }
            else if (!editing && (e.which == 9 && !e.shiftKey))
            {
                // Tab
                e.preventDefault();
                c = currCell.next();
            }
            else if (!editing && (e.which == 9 && e.shiftKey))
            {
                // Shift + Tab
                e.preventDefault();
                c = currCell.prev();
            }

            // If we didn't hit a boundary, update the current cell
            if (c.length > 0) {
                currCell = c;
                currCell.focus();
            }
        });

        // User can cancel edit by pressing escape
        //$('#edit').keydown(function (e) {
        //    if (editing && e.which == 27) {
        //        editing = false;
        //        $('#edit').hide();
        //        currCell.toggleClass("editing");
        //        currCell.focus();
        //    }
        //});
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <table>
        <thead>
            <tr>
                <th>Col 1</th>
                <th>Col 2</th>
                <th>Col 3</th>
                <th>Col 4</th>
                <th>Col 5</th>
                <th>Col 6</th>
                <th>Col 7</th>
                <th>Col 8</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td tabindex="1">1</td>
                <td tabindex="2">2</td>
                <td tabindex="3">3</td>
                <td tabindex="4">4</td>
                <td tabindex="5">5</td>
                <td tabindex="6">6</td>
                <td tabindex="7">7</td>
                <td tabindex="8">8</td>
            </tr>
            <tr>
                <td tabindex="10">10</td>
                <td tabindex="11">11</td>
                <td tabindex="12">12</td>
                <td tabindex="13">13</td>
                <td tabindex="14">14</td>
                <td tabindex="15">15</td>
                <td tabindex="16">16</td>
                <td tabindex="17">17</td>
            </tr>
            <tr>
                <td tabindex="10">10</td>
                <td tabindex="11">11</td>
                <td tabindex="12">12</td>
                <td tabindex="13">13</td>
                <td tabindex="14">14</td>
                <td tabindex="15">15</td>
                <td tabindex="16">16</td>
                <td tabindex="17">17</td>
            </tr>
        </tbody>
    </table>

    <div id="edit">
        <form>
            <input type="text" id="text" value="To edit..." />
            <input type="submit" value="Save" />
        </form>
    </div>

</body>
</html>
