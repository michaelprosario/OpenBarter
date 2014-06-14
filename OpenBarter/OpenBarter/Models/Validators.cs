using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace OpenBarter.Models
{
    public class ForTradeValidator : AbstractValidator<ForTrade>
    {
        public ForTradeValidator()
        {
            RuleFor(r => r.CategoryID).GreaterThan(0);
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.MarketID).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.OwnerID).GreaterThan(0);
            RuleFor(r => r.Status).NotEmpty();
            RuleFor(r => r.UpdatedBy).NotEmpty();
            RuleFor(r => r.CreatedBy).NotEmpty();
        }
    }

    public class OfferForWantValidator : AbstractValidator<OfferForWant>
    {
        public OfferForWantValidator()
        {
            RuleFor(r => r.CreatedBy).NotEmpty();
            RuleFor(r => r.UpdatedBy).NotEmpty();
            RuleFor(r => r.Offer).NotEmpty();
        }
    }

    public class OfferForTradeValidator : AbstractValidator<OfferForTrade>
    {
        public OfferForTradeValidator()
        {
            RuleFor(r => r.CreatedBy).NotEmpty();
            RuleFor(r => r.UpdatedBy).NotEmpty();
            RuleFor(r => r.Offer).NotEmpty();
        }
    }



    public class UserDataValidator : AbstractValidator<UserData>
    {
        public UserDataValidator()
        {
            RuleFor(r => r.AboutMe).NotEmpty();
            RuleFor(r => r.CreatedBy).NotEmpty();
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.UpdatedBy).NotEmpty();
            RuleFor(r => r.UserProfileID).GreaterThan(0);
        }
    }

    public class WantValidator : AbstractValidator<Want>
    {
        public WantValidator()
        {
            RuleFor(r => r.CategoryID).GreaterThan(0);
            RuleFor(r => r.CreatedBy).NotEmpty();
            RuleFor(r => r.MarketID).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.OwnerID).NotEmpty();
            RuleFor(r => r.Status).NotEmpty();
            RuleFor(r => r.UpdatedBy).NotEmpty();

        }
    }




}