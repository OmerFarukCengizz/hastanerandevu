//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hastanerandevu.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ilceler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ilceler()
        {
            this.hastaneler = new HashSet<hastaneler>();
        }
    
        public int ILCEID { get; set; }
        public int SEHIRID { get; set; }
        public string ILCEAD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hastaneler> hastaneler { get; set; }
        public virtual sehirler sehirler { get; set; }
    }
}
