using ProjectWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;

namespace ProjectWebsite.SiteTools
{
    public class StripeTools
    {
        public static string GetTokenId(PaymentViewModel model)
        {
            var myToken = new StripeTokenCreateOptions();

            myToken.Card = new StripeCreditCardOptions()
            {
                Number = model.CardNum,
                ExpirationMonth = model.ExpiryMonth,
                ExpirationYear = model.ExpiryYear,
                Cvc = model.cvv.ToString(),
                AddressLine1 = model.Address1,
                AddressLine2 = model.Address2,
                AddressCity = model.City,
                AddressState = model.County,
                Currency = "eur",
                Name = model.CardHolder,

            };

            var tokenService = new StripeTokenService();
            var stripeToken = tokenService.Create(myToken);

            return stripeToken.Id;
                      
        }

        public static string Charge(string token)
        {
            var charge = new StripeChargeCreateOptions
            {
                Amount = 5000,
                Currency = "eur",
                Description = "IT Weather Premium",
                SourceTokenOrExistingSourceId = token
            };

            var chargeService = new StripeChargeService();
            var stripeCharge = chargeService.Create(charge);

            return stripeCharge.Id;
        }
    }
}