using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Security;
using Intuit.Ipp.Core;
using System.Configuration;
using System.Net;
using System.Globalization;
using System.IO;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Data;


namespace SampleApp_CRUD_DotNet
{
    public class QBOServiceInitializer
    {

        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string realmId { get; set; }

        public QBOServiceInitializer()
        {
        }

        public QBOServiceInitializer(string accessToken, string refreshToken, string realmId)
        {
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
            this.realmId = realmId;
        }

        //private static void Initialize()
        //{
           

        //}

        public ServiceContext InitializeQBOServiceContextUsingoAuth()
        {
            //Initialize();
            OAuth2RequestValidator reqValidator = new OAuth2RequestValidator(this.accessToken);
            ServiceContext context = new ServiceContext(this.realmId, IntuitServicesType.QBO, reqValidator);
            
            //MinorVersion represents the latest features/fields in the xsd supported by the QBO apis.
            //Read more details here- https://developer.intuit.com/docs/0100_quickbooks_online/0200_dev_guides/accounting/querying_data

            context.IppConfiguration.MinorVersion.Qbo = "12";
            return context;
        }
    }
}
