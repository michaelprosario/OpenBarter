using OpenBarter.Filters;
using OpenBarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace OpenBarter.Controllers
{
    [InitializeSimpleMembership]
    public class ListingController : Controller
    {

        public ActionResult ForTrade()
        {
            ForTradeRepo t = new ForTradeRepo();
            CategoryRepo categories = new CategoryRepo();

            List<ForTrade> list = t.GetRecordsForMarket(Constants.DefaultMarket);
            

            ViewBag.Categories = categories.GetAll();


            return View(list);
        }

        public ActionResult Wanted()
        {
            WantRepo t = new WantRepo();
            CategoryRepo categories = new CategoryRepo();

            List<Want> list = t.GetRecordsForMarket(Constants.DefaultMarket);


            ViewBag.Categories = categories.GetAll();


            return View(list);
        }

        public ActionResult ViewForTrade(int id)
        {
            ForTradeRepo t = new ForTradeRepo();
            ForTrade r = t.GetRecord(id);

            ViewBag.Offers = new OfferForTradeRepo().GetOffers(id);


            return View(r);
        }

        public ActionResult ViewWantedItem(int id)
        {
            WantRepo t = new WantRepo();
            var r = t.GetRecord(id);

            ViewBag.Offers = new OfferForWantRepo().GetOffers(id);

            return View(r);
        }

        [Authorize]
        public ActionResult AddOfferForTrade(int id)
        {
            OfferForTrade offer = new OfferForTrade();
            offer.ForTradeID = id;
            return View(offer);
        }

        //http://localhost:50612/listing/DeleteOfferForTrade?offerID=2&forTradeID=8
        [Authorize]
        public ActionResult DeleteOfferForTrade(int offerID, int forTradeID)
        {
            OfferForTradeRepo offers = new OfferForTradeRepo();
            offers.DeleteRecord(offerID);

            return RedirectToAction("ViewForTrade", "listing", new { id = forTradeID });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOfferForTrade(FormCollection collection)
        {
            OfferForTradeRepo offers = new OfferForTradeRepo();
            OfferForTrade offer = new OfferForTrade();

            try
            {
                offer.AcceptFlag = false;
                offer.CreatedAt = DateTime.Now;
                offer.CreatedBy = WebSecurity.CurrentUserName;
                offer.Offer = collection["Offer"];
                offer.OwnerID = WebSecurity.CurrentUserId;
                offer.UpdatedAt = DateTime.Now;
                offer.UpdatedBy = WebSecurity.CurrentUserName;
                offer.ForTradeID = int.Parse(collection["ForTradeID"]);
                offers.AddRecord(offer);


                return RedirectToAction("ViewForTrade", "listing", new { id = offer.ForTradeID } );
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }


                return View(offer);
            }

            return View();
        }

        [Authorize]
        public ActionResult AddOfferForWant(int id)
        {
            OfferForWant offer = new OfferForWant();
            offer.WantID = id;
            return View(offer);
        }

        
        [Authorize]
        public ActionResult DeleteOfferForWant(int offerID, int wantID)
        {
            OfferForWantRepo offers = new OfferForWantRepo();
            offers.DeleteRecord(offerID);

            return RedirectToAction("ViewWantedItem", "listing", new { id = wantID });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOfferForWant(FormCollection collection)
        {
            OfferForWantRepo offers = new OfferForWantRepo();
            OfferForWant offer = new OfferForWant();

            try
            {
                offer.AcceptFlag = false;
                offer.CreatedAt = DateTime.Now;
                offer.CreatedBy = WebSecurity.CurrentUserName;
                offer.Offer = collection["Offer"];
                offer.OwnerID = WebSecurity.CurrentUserId;
                offer.UpdatedAt = DateTime.Now;
                offer.UpdatedBy = WebSecurity.CurrentUserName;
                offer.WantID = int.Parse(collection["WantID"]);
                offers.AddRecord(offer);


                return RedirectToAction("ViewWantedItem", "listing", new { id = offer.WantID });
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }


                return View(offer);
            }

            
        }


    }
}
