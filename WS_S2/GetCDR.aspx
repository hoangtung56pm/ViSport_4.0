<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetCDR.aspx.cs" Inherits="GetCDR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0px auto">
    <table align="center" cellpadding="0" cellspacing="0" border="0" width="60%">
            <tr>
                <td><asp:Label ID="lblUpdateStatus" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td align="center">
                     <b>Chọn đối tác &nbsp;</b> 

                     <asp:DropDownList ID="DropDoiTac" runat="server" Width="200">                        
                    </asp:DropDownList>
                   

                </td>
            </tr>

            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td align="center" style="background: #6d84a2; font-size: 30px; font-weight: bold">
                    Cập nhật lại CDR Cho đối tác
                </td>
            </tr>
            <tr>
                <td height="10"></td>
            </tr>

            <tr>
                <td>
                     
                    <table align="center" cellpadding="0" cellspacing="0" border="0" width="80%">
                        <tr>
                            <td>Năm </td>    
                            <td><asp:DropDownList ID="dgrNam" runat="server" Width="100"></asp:DropDownList></td>
                            
                            <td>GPC</td>
                            <td><asp:CheckBox runat="server" ID="CkGPC"  Checked="true"/></td>
                        </tr>
                        
                        <tr>
                            <td height="5"></td>
                        </tr>
                        
                        <tr>
                            <td>Tháng </td>    
                            <td><asp:DropDownList ID="dgrThang" runat="server" Width="100"></asp:DropDownList></td>
                            <td>VMS</td>
                            <td><asp:CheckBox runat="server" ID="CkVMS" Checked="true"/></td>
                        </tr>
                        
                        <tr>
                            <td height="5"></td>
                        </tr>

                        <tr>
                            <td>Ngày </td>    
                            <td><asp:DropDownList ID="dgrNgay" runat="server" Width="100"></asp:DropDownList></td>
                            <td>VNM</td>
                            <td><asp:CheckBox runat="server" ID="CkVNM" Checked="true"/></td>
                            
                        </tr>
                        
                       
                        <tr>
                            <td height="5"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:Button ID="btnUpdateLog" runat="server" Text="Cập nhật log VMS,GPC" CssClass="Button" OnClick="btnUpdateLog_Click"/>&nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="Export File" CssClass="Button" OnClick="btnExport_Click"/>&nbsp;
                                <asp:Button ID="btnUpdate" runat="server" Text="Export all CDR file" CssClass="Button" OnClick="btnUpdate_Click"/>&nbsp;
                                
                            </td>
                        </tr>
                        
                        <tr>
                            <td height="5"></td>
                        </tr>

                    </table>

                </td>
            </tr>            
            
        </table>
               
        <asp:Button runat="server" ID="btUpdateLog" Text="Update Log" OnClick="btUpdateLog_Click" Visible="false" />
        <asp:DropDownList runat="server" ID="drop_Service_type">
            <asp:ListItem Text="1002" Value="1002"></asp:ListItem>
            <asp:ListItem Text="1007" Value="1007"></asp:ListItem>
            <asp:ListItem Text="1008" Value="1008"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button runat="server" ID="btnUpdate_User" Text="Update user" OnClick="btnUpdate_User_Click"/>
    </div>

    </form>
</body>
</html>
