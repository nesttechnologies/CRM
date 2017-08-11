<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_Dashboard.aspx.cs" Inherits="Charts001.JSON1" %> <!-- Inherits="BasicExample_BasicChart" -->

 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  <!-- <!DOCTYPE html> -->

<html >  <!-- xmlns="http://www.w3.org/1999/xhtml" -->
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cyber Resilience Model</title>
    <script type="text/javascript" src="../fusioncharts/fusioncharts.js"></script>
    <script type="text/javascript" src="../fusioncharts/themes/fusioncharts.theme.fint.js"></script>

    <!-- <script type="text/javascript" src="fusioncharts/js/themes/fusioncharts.theme.fint.js"></script> -->
    <style type="text/css">
        #Select1 {
            height: 4px;
            width: 582px;
        }
        #Text1 {
            width: 560px;
        }
    </style>
</head>
<body style="background-color: #000000">
    <form id="form1" runat="server" defaultbutton="SubmitButton" style="vertical-align: middle; height: 389px; width: 100%; ">


            <div style="width:100%;"> 

        <div style="float:left; width:100%; font-family: 'Arial Narrow'; color: #FFFFFF;"> 
            Company Name
            <asp:TextBox id="Text1" type="text" runat="server" OnTextChanged="Text1_TextChanged" Text="Walmart"/>
            <asp:Button ID="SubmitButton" runat="server" Text="Go"
    OnClick = "SearchData" /></div> &nbsp;<br />
                <div style="float:right; width:40%; font-family: 'Arial Narrow'; color: #000000;">
                <iframe width="640" height="640" src="https://cybermap.kaspersky.com/en/widget/dynamic/dark" frameborder="0"></iframe>
            </div>
                <div style="float:left; width:60%; font-family: 'Arial Narrow'; color: #000000;">
            <div style="float:left; width:33%;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                  </div>
                <div style="float:left; width:33%;">
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                      </div>
                    <div style="float:left; width:33%;">
                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                          </div>
                    </div>
                &nbsp;<br />
                <div style="float:left; width:60%; font-family: 'Arial Narrow'; color: #000000;">
                        <div style="float:left; width:33%;">
                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                      </div>
                
                            <div style="float:left; width:33%;">
                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
              </div>
                    <div style="float:left; width:33%;">
                <asp:Literal ID="Literal6" runat="server"></asp:Literal>
              </div>
                    </div>
                &nbsp; <br />
            <div style="float:left; width:60%; font-family: 'Arial Narrow'; color: #000000;">
                        <div style="float:left; width:33%;">
                <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                  </div>
                
                            <div style="float:left; width:33%;">
                <asp:Literal ID="Literal8" runat="server"></asp:Literal>
              </div>
                    <div style="float:left; width:33%;">
            <asp:Literal ID="Literal9" runat="server"></asp:Literal>
              </div>
                    </div>
        
                </div>
    </form>
</body>
</html>