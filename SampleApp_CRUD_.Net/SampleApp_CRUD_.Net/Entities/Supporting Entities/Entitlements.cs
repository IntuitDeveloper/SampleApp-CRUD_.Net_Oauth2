using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Ipp.EntitlementService;
using Intuit.Ipp.Data;
using Intuit.Ipp.Core;

namespace SampleApp_CRUD_.Net
{
    public class Entitlements
    {
        public EntitlementsResponse EntitlementsGetSync(ServiceContext context, string baseUrl)
        {
            EntitlementService service = new EntitlementService(context);
            EntitlementsResponse entitlements = service.GetEntitlements(baseUrl);
            return entitlements;
        }
    }
}