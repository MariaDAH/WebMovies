//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMovies.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favorite
    {
        public long favoriteId { get; set; }
        public long usrId { get; set; }
        public long linkId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual Link Link { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
