﻿using System;
using System.Collections.Generic;
using Intuit.Ipp.Data;
using Intuit.Ipp.Core;
using Intuit.Ipp.GlobalTaxService;


namespace SampleApp_CRUD_DotNet
{
    public class GlobalTaxServiceCRUD
    {
        #region Sync Methods

        #region  Add Operations




        public void TaxCodeAddTestUsingoAuth(ServiceContext qboContextoAuth)
        {
            String guid = Helper.GetGuid();
            GlobalTaxService taxSvc = new GlobalTaxService(qboContextoAuth);
            TaxService taxCodetobeAdded = new TaxService();
            taxCodetobeAdded.TaxCode = "taxC_" + guid;

            TaxAgency taxagency = Helper.FindOrAdd<TaxAgency>(qboContextoAuth, new TaxAgency());


            List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
            TaxRateDetails taxdetail1 = new TaxRateDetails();
            taxdetail1.TaxRateName = "taxR1_" + guid;
            taxdetail1.RateValue = 3m;
            taxdetail1.RateValueSpecified = true;
            taxdetail1.TaxAgencyId = taxagency.Id.ToString();
            taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail1.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail1);

            TaxRateDetails taxdetail2 = new TaxRateDetails();
            taxdetail2.TaxRateName = "taxR2_" + guid;
            taxdetail2.RateValue = 2m;
            taxdetail2.RateValueSpecified = true;
            taxdetail2.TaxAgencyId = taxagency.Id.ToString();
            taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail2.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail2);

            //TaxRateDetails taxdetail3 = new TaxRateDetails();
            //taxdetail3.TaxRateName = "rate298";
            //taxdetail3.TaxRateId = "2";

            //lstTaxRate.Add(taxdetail3);

            taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();

            Intuit.Ipp.Data.TaxService taxCodeAdded = taxSvc.AddTaxCode(taxCodetobeAdded);
           
        }

        #endregion

        #endregion
    }
}