<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleApp_CRUD_DotNet.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <% if (dictionary.ContainsKey("accessToken"))
            {
                Response.Write("<script> window.opener.location.reload();window.close(); </script>");
            }
        %> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <h3>Welcome to the Intuit CRUD - OAuth2 Sample App!</h3>
                Before using this app, please make sure you do the following:
                <ul>
                    <li>Note:This sample app is just for reference and works great for Chrome and Firefox. IE throws up some javascript errors so it is advisable to test for the specific browser you are working with and make desired changes.</li>
                    <li>Update your Client ID, Client Secret, Redirect Url (found on <a href="https://developer.intuit.com">developer.intuit.com</a>)
                    and app environment(prod/sandbox) in web.config</li>
                    <li>Update your Log file Path in web.config</li>
                    <li>For this sample app we have used only OAuth flow. There are other sample apps for Oauth2 for SIWI, GetAppNow and C2QB flow on https://github.com/IntuitDeveloper/
                    </li>
                </ul>
    
                 <p>&nbsp;</p>
            </div>
            <div id="connect" runat="server">
                <asp:ImageButton id="btnOAuth" runat="server" AlternateText="OAuth"
                   ImageAlign="left"
                   ImageUrl="Images/C2QB_white_btn_lg_default.png"
                    CssClass="font-size:14px; border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px" OnClick="btnOAuth_Click"/>
                <br /><br /><br />
            </div>
            <div id="revoke" runat="server" visible ="false">
                <p>
                <asp:label runat="server" id="lblConnected" visible="false">"Your application is connected!"</asp:label>
                </p>  

                <asp:Button id="btnQBOAPICall" runat="server" Text="Call QBO API" OnClick="btnQBOAPICall_Click"/>
                <br /><br /><br />

                <p><asp:label runat="server" id="lblQBOCall" visible="false"></asp:label></p>
                <br /><br /><br />
            </div>
        </form>
    </body>
</html>