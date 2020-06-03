<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuLieuLichSu.aspx.cs" Inherits="WebPredict.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            Du Lieu Lich Su :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DuLieuDuDoan.aspx">Du Lieu Du Doan</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
