using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Threading;
using Intuit.Ipp.QueryFilter;


using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class CompanyInfoTest
    {
        ServiceContext qboContextoAuth = null;
        
        [TestInitialize]
        public void MyTestInitializer()
        {
            
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
            
            
        }

        

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CompanyInfoFindAllTestUsingoAuth()
        {
            SeriLogger.log.Write(Serilog.Events.LogEventLevel.Verbose, "COmpanyInfo FindAll test started");

            //Making sure that at least one entity is already present
            // CompanyInfoAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            List<CompanyInfo> companyInfos = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo(), 1, 500);
            Assert.IsNotNull(companyInfos);
            Assert.IsTrue(companyInfos.Count<CompanyInfo>() > 0);
        }

        #endregion

      

    }
}
