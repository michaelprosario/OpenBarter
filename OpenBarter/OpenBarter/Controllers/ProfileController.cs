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
    public class ProfileController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {        
            int currentUserID = WebSecurity.CurrentUserId;

            var wants = new WantRepo();
            var forTrades = new ForTradeRepo();
            var userData = new UserDataRepo();

            List<Want> myWantList = wants.GetRecordsByUserID(currentUserID);
            List<ForTrade> myForTradeList = forTrades.GetRecordsByUserID(currentUserID);
            UserData myUserData = userData.GetUserDataByUserID(currentUserID);

            ViewBag.MyWantList = myWantList;
            ViewBag.MyForTradeList = myForTradeList;
            ViewBag.MyUserData = myUserData;

            return View();
        }

        [Authorize]
        public ActionResult CreateWant()
        {
            var categories = new CategoryRepo();
            var catList = categories.GetAll();
            ViewBag.CategoryList = catList;

            var aWant = new Want();
            aWant.MarketID = 1;
            aWant.Name = "";
            aWant.OwnerID = WebSecurity.CurrentUserId;
            aWant.Status = "open";
            aWant.Tags = "";
            aWant.CreatedBy = "mrosario";
            aWant.UpdatedBy = "mrosario";
            
            return View(aWant);
        }


        [Authorize]
        public ActionResult CreateForTrade()
        {
            var categories = new CategoryRepo();
            var catList = categories.GetAll();
            ViewBag.CategoryList = catList;

            var forTrade = new ForTrade();
            forTrade.MarketID = Constants.DefaultMarket;
            forTrade.Name = "";
            forTrade.OwnerID = WebSecurity.CurrentUserId;
            forTrade.Status = Constants.DefaultWantStatus;
            forTrade.Tags = "";
            forTrade.CreatedBy = "mrosario";
            forTrade.UpdatedBy = "mrosario";

            return View(forTrade);
        }

        [HttpPost]
        public ActionResult CreateForTrade(FormCollection collection)
        {
            ForTradeRepo forTrades = new ForTradeRepo();
            ForTrade forTrade = new ForTrade();

            try
            {
                forTrade.CategoryID = int.Parse(collection["category_id"]);
                forTrade.CreatedAt = DateTime.Now;
                forTrade.CreatedBy = WebSecurity.CurrentUserName;
                forTrade.Description = collection["Description"];
                forTrade.MarketID = Constants.DefaultMarket;
                forTrade.Name = collection["Name"];
                forTrade.OwnerID = WebSecurity.CurrentUserId;
                forTrade.Status = Constants.DefaultWantStatus;
                forTrade.Tags = collection["Tags"];
                forTrade.UpdatedAt = DateTime.Now;
                forTrade.UpdatedBy = WebSecurity.CurrentUserName;

                forTrades.AddRecord(forTrade);


                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var categories = new CategoryRepo();
                var catList = categories.GetAll();
                ViewBag.CategoryList = catList;

                return View(forTrade);
            }
        }




        [HttpPost]
        public ActionResult CreateWant(FormCollection collection)
        {
            WantRepo wants = new WantRepo();
            Want aWant = new Want();

            try
            {
                aWant.CategoryID = int.Parse(collection["category_id"]);
                aWant.CreatedAt = DateTime.Now;
                aWant.CreatedBy = WebSecurity.CurrentUserName;
                aWant.Description = collection["Description"];
                aWant.MarketID = Constants.DefaultMarket;
                aWant.Name = collection["Name"];
                aWant.OwnerID = WebSecurity.CurrentUserId;
                aWant.Status = Constants.DefaultWantStatus;
                aWant.Tags = collection["Tags"];
                aWant.UpdatedAt = DateTime.Now;
                aWant.UpdatedBy = WebSecurity.CurrentUserName;
               
                wants.AddRecord(aWant);


                return RedirectToAction("Index");
            }
            catch(FluentValidation.ValidationException validationEx)
            {
                foreach(var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var categories = new CategoryRepo();
                var catList = categories.GetAll();
                ViewBag.CategoryList = catList;

                return View(aWant);
            }
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteWant(int id)
        {
            WantRepo wants = new WantRepo();
            wants.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteForTrade(int id)
        {
            ForTradeRepo forTrades = new ForTradeRepo();
            forTrades.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult EditWant(int id)
        {
            var categories = new CategoryRepo();
            var catList = categories.GetAll();
            ViewBag.CategoryList = catList;

            var t = new WantRepo();
            var r = t.GetRecord(id);

            return View(r);
        }


        [Authorize]
        public ActionResult EditForTrade(int id)
        {
            var categories = new CategoryRepo();
            var catList = categories.GetAll();
            ViewBag.CategoryList = catList;

            var forTrades = new ForTradeRepo();
            var item = forTrades.GetRecord(id);

            return View(item);
        }


        [HttpPost]
        public ActionResult EditForTrade(FormCollection collection)
        {
            ForTradeRepo forTrades = new ForTradeRepo();
            ForTrade item = new ForTrade();

            int id = int.Parse(collection["Id"]);

            try
            {
                item = forTrades.GetRecord(id);
                item.CategoryID = int.Parse(collection["category_id"]);
                item.Description = collection["Description"];
                item.Name = collection["Name"];
                item.Tags = collection["Tags"];
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = WebSecurity.CurrentUserName;

                forTrades.UpdateRecord(item);


                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var categories = new CategoryRepo();
                var catList = categories.GetAll();
                ViewBag.CategoryList = catList;

                return View(item);
            }
        }

        [HttpPost]
        public ActionResult EditWant(FormCollection collection)
        {
            WantRepo wants = new WantRepo();
            Want aWant = new Want();

            int id = int.Parse(collection["Id"]);

            try
            {
                aWant = wants.GetRecord(id);
                aWant.CategoryID = int.Parse(collection["category_id"]);
                aWant.Description = collection["Description"];
                aWant.Name = collection["Name"];
                aWant.Tags = collection["Tags"];
                aWant.UpdatedAt = DateTime.Now;
                aWant.UpdatedBy = WebSecurity.CurrentUserName;

                wants.UpdateRecord(aWant);


                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var categories = new CategoryRepo();
                var catList = categories.GetAll();
                ViewBag.CategoryList = catList;

                return View(aWant);
            }
        }

        [Authorize]
        public ActionResult EditMyUserData()
        {
            int currentUserID = WebSecurity.CurrentUserId;

            UserDataRepo t = new UserDataRepo();
            var r = t.GetUserDataByUserID(currentUserID);
            return View(r);
            
        }

        [HttpPost]
        public ActionResult EditMyUserData(FormCollection collection)
        {
            UserDataRepo t = new UserDataRepo();
            UserData r = new UserData();

            int id = int.Parse(collection["Id"]);

            try
            {
                r = t.GetRecord(id);
                r.LinkedInURL = collection["LinkedInURL"];
                r.TwitterURL = collection["TwitterURL"];
                r.UpdatedAt = DateTime.Now;
                r.UpdatedBy = WebSecurity.CurrentUserName;
                r.WebsiteURL = collection["WebsiteURL"];
                r.AboutMe = collection["AboutMe"];
                r.AmazonURL = collection["AmazonURL"];
                r.EbayURL = collection["EbayURL"];
                r.Email = collection["Email"].Trim();
                r.EtsyURL = collection["EtsyURL"];
                

                t.UpdateRecord(r);


                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException validationEx)
            {
                foreach (var error in validationEx.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }



                return View(r);
            }
        }



    }
}
