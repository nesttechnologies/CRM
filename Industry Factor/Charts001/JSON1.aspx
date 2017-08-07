<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSON1.aspx.cs" Inherits="Charts001.JSON1" %> <!-- Inherits="BasicExample_BasicChart" -->

 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  <!-- <!DOCTYPE html> -->

<html >  <!-- xmlns="http://www.w3.org/1999/xhtml" -->
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FusionCharts - Simple</title>
    <script type="text/javascript" src="../fusioncharts/fusioncharts.js"></script>
    <script type="text/javascript" src="../fusioncharts/themes/fusioncharts.theme.fint.js"></script>

    <!-- <script type="text/javascript" src="fusioncharts/js/themes/fusioncharts.theme.fint.js"></script> -->
</head>
<body>
    <form id="form1" runat="server" defaultbutton="SubmitButton" style="vertical-align: middle; height: 100%; width: 100%; ">
            <div style="width:100%;">
        <div style="float:left; width:20%;">
            Provide company name
    <asp:Textbox ID="Text1" runat="server"></asp:Textbox>

           <asp:Button ID="SubmitButton" runat="server" Text="Button"
    OnClick = "SearchData" />

         </div>
        <div style="float:right; width:40%;">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
          
        <div style="float:left; width:40%;">
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
        </div>
            </div>


            <div style="width:100%;"> 
        <div style="float:left; width:20%;">
                </div>

        <div style="float:right; width:40%;"> 
    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
        </div>

        <div style="float:right; width:40%;">
    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
        </div> 
            </div>
                
    </form>
</body>
</html>




    
   