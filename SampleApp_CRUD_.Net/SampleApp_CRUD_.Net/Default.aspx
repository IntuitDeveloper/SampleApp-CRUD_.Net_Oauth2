<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleApp_CRUD_DotNet.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <% if (dictionary.ContainsKey("accessToken") && dictionary["callMadeBy"]!="OpenId")
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
    <li>
      Update your Client ID, Client Secret, Redirect Url (found on <a href="https://developer.intuit.com">developer.intuit.com</a>)
        in web.config</li>
      <li>
          Update your Log file Path in web.config</li>
      <li>
          For this sample app we have used only Get App now flow. There are other sample apps for Oauth2 for SIWI, GetAppNow and C@QB flow on https://github.com/IntuitDeveloper/
           </li>
        </ul>

  

 
    
         <p>
             &nbsp;</p>

  </div>
 <div id="connect" runat="server" visible ="false">
    
 

   <!-- Get App Now -->
   <b>Get App Now</b><br />
   <asp:ImageButton id="btnOpenId" runat="server" AlternateText="Get App Now"
           ImageAlign="left"
           ImageUrl="Images/Get_App.png"
           OnClick="ImgOpenId_Click" CssClass="font-size:14px; border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>
     <br /><br /><br />
 
    
    </div>

 <div id="revoke" runat="server" visible ="false">
    <p>
    <asp:label runat="server" id="lblConnected" visible="false">"Your application is connected!"</asp:label>
    </p>  
     <asp:ImageButton id="btnQBOAPICall" runat="server" AlternateText="Call QBO API"
           ImageAlign="left"
       
           OnClick="ImgQBOAPICall_Click" CssClass="font-size:14px; border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>
     <br /><br />
     <p>
    <asp:label runat="server" id="lblQBOCall" visible="false"></asp:label>
    </p>
     <br />

       <br /><br /><br />
     <asp:ImageButton id="btnRevoke" runat="server" AlternateText="Revoke Tokens"
           ImageAlign="left"
           
           OnClick="ImgRevoke_Click" CssClass="font-size:14px; border: 1px solid grey; padding: 10px; color: red" Height="40px" Width="200px"/>
       <br /><br /><br />
   
</div>
    </form>

    
</body>
</html>