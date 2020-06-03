<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuLieuDuDoan.aspx.cs" Inherits="WebPredict.DuLieuDuDoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Du Lieu Du Doan :&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DuLieuLichSu.aspx">Du Lieu Lich Su</asp:HyperLink>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
