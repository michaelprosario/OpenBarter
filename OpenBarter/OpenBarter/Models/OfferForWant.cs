//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenBarter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferForWant
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public string Offer { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool AcceptFlag { get; set; }
        public int WantID { get; set; }
    }
}
