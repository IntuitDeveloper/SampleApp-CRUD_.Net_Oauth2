

/******************************************************
 * Intuit CRUD sample app for Oauth2 using Intuit .Net SDK
 * RFC docs- https://tools.ietf.org/html/rfc6749
 * ****************************************************/

//https://stackoverflow.com/questions/23562044/window-opener-is-undefined-on-internet-explorer/26359243#26359243
//IE issue- https://stackoverflow.com/questions/7648231/javascript-issue-in-ie-with-window-opener

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using System.Web;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Threading.Tasks;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Core;

namespace SampleApp_CRUD_DotNet
{
    public partial class Default : Page
    {
        // OAuth2 client configuration
        static string redirectURI = ConfigurationManager.AppSettings["redirectURI"];
        static string clientID = ConfigurationManager.AppSettings["clientID"];
        static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        static string env = ConfigurationManager.AppSettings["appEnvironment"];
        static OAuth2Client client = new OAuth2Client(clientID, clientSecret, redirectURI, env);
        static string logPath = ConfigurationManager.AppSettings["logPath"];
        static string realmId = "";
        static string authCode;
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public HttpContext CurrentContext { get; private set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!dictionary.ContainsKey("accessToken"))
            {
                //display connect buttons 
                btnOAuth.Visible = true;
                revoke.Visible = false;
                lblConnected.Visible = false;
            }
            else
            {
                //display revoke button
                btnOAuth.Visible = false;
                revoke.Visible = true;
                lblConnected.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AsyncMode = true;
            if (!dictionary.ContainsKey("accessToken"))
            {
                if (Request.QueryString.Count > 0)
                {
                    var response = new AuthorizeResponse(Request.QueryString.ToString());
                    if (response.State != null)
                    {
                        if (client.CSRFToken == response.State)
                        {
                            if (response.RealmId != null)
                            {
                                realmId = response.RealmId;
                                if (!dictionary.ContainsKey("realmId"))
                                {
                                    dictionary.Add("realmId", realmId);
                                }
                            }

                            if (response.Code != null)
                            {
                                authCode = response.Code;
                                output("Authorization code obtained.");
                                PageAsyncTask t = new PageAsyncTask(performCodeExchange);
                                Page.RegisterAsyncTask(t);
                                Page.ExecuteRegisteredAsyncTasks();
                            }
                        }
                        else
                        {
                            output("Invalid State");
                            dictionary.Clear();
                        }
                    }
                }
            }
            else
            {
                connect.Visible = false;
                revoke.Visible = true;
            }
        }

        #region button click events
        protected void btnOAuth_Click(object sender, ImageClickEventArgs e)
        {
            if (!dictionary.ContainsKey("accessToken"))
            {
                List<OidcScopes> scopes = new List<OidcScopes>();
                scopes.Add(OidcScopes.Accounting);
                var authorizationRequest = client.GetAuthorizationURL(scopes);
                Response.Redirect(authorizationRequest, "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
            }
        }

        protected async void btnQBOAPICall_Click(object sender, EventArgs e)
        {
            await qboApiCall();
        }
        #endregion

        public async Task performCodeExchange()
        {
            output("Exchanging code for tokens.");
            try
            {
                var tokenResp = await client.GetBearerTokenAsync(authCode);
                if (!dictionary.ContainsKey("accessToken"))
                    dictionary.Add("accessToken", tokenResp.AccessToken);
                else
                    dictionary["accessToken"] = tokenResp.AccessToken;

                if (!dictionary.ContainsKey("refreshToken"))
                    dictionary.Add("refreshToken", tokenResp.RefreshToken);
                else
                    dictionary["refreshToken"] = tokenResp.RefreshToken;
            }
            catch(Exception ex)
            {
                output(ex.Message);
            }
        }


        /// <summary>
        /// Test QBO api call
        /// </summary>
        public async Task qboApiCall()
        {
            try
            {
                if ((dictionary.ContainsKey("accessToken")) && (dictionary.ContainsKey("accessToken")) && (dictionary.ContainsKey("realmId")))
                {
                    output("Making QBO API Call.");
                    QBOServiceInitializer initialize = new QBOServiceInitializer(dictionary["accessToken"], dictionary["refreshToken"], dictionary["realmId"]);
                    ServiceContext servicecontext = initialize.InitializeQBOServiceContextUsingoAuth();
                    TestQBOCalls.allqbocalls(servicecontext);

                    output("QBO calls successful.");
                    lblQBOCall.Visible = true;
                    lblQBOCall.Text = "QBO Call successful";
                }
            }
            catch (IdsException ex)
            {
                if (ex.Message == "Unauthorized-401")
                {
                    output("Invalid/Expired Access Token.");

                    var tokenResp = await client.RefreshTokenAsync(dictionary["refreshToken"]);
                    if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
                    {
                        dictionary["accessToken"] = tokenResp.AccessToken;
                        dictionary["refreshToken"] = tokenResp.RefreshToken;
                        await qboApiCall();
                    }
                    else
                    {
                        output("Error while refreshing tokens: " + tokenResp.Raw);
                    }
                }
                else
                {
                    output(ex.Message);
                }
            }
            catch (Exception ex)
            {
                output("Invalid/Expired Access Token.");
            }
        }


        #region helper methods for logging
        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        public string GetLogPath()
        {
            try
            {
                if (logPath == "")
                {
                    logPath = System.Environment.GetEnvironmentVariable("TEMP");
                    if (!logPath.EndsWith("\\")) logPath += "\\";
                }
            }
            catch
            {
                output("Log error path not found.");
            }
            return logPath;
        }

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="logMsg">string to be appended</param>
        public void output(string logMsg)
        {
            //Console.WriteLine(logMsg);

            System.IO.StreamWriter sw = System.IO.File.AppendText(GetLogPath() + "OAuth2SampleAppLogs.txt");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, logMsg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }

        #endregion  
    }

    /// <summary>
    /// Helper for calling self
    /// </summary>
    public static class ResponseHelper
    {
        public static void Redirect(this HttpResponse response, string url, string target, string windowFeatures)
        {
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures))
            {
                response.Redirect(url);
            }
            else
            {
                Page page = (Page)HttpContext.Current.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }
                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }
    }
}