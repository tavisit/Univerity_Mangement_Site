//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SINU
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string surname { get; set; }
        public string lastname { get; set; }
        public Nullable<System.DateTime> birth_date { get; set; }
        public string photo_url { get; set; }
        public string email { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Student Student { get; set; }
    }
}
