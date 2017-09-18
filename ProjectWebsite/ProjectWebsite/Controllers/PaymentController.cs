using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebsite.Controllers
{
    public class PaymentController : Controller
    {
        [Authorize]
        [HttpGet]
        // GET: Payment
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {            
            var chargeId = "";

            if(ModelState.IsValid)
            {
                try
                {
                    var tokenId = SiteTools.StripeTools.GetTokenId(model);
                    chargeId = SiteTools.StripeTools.Charge(tokenId);

                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;                    
                }

            }           

            return View();
        }
    }
}