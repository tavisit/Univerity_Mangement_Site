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
    
    public partial class Series
    {
        public int id { get; set; }
        public int id_student { get; set; }
        public int id_head_teacher { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
