//------------------------------------------------------------------------------
// <auto-generated>
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.randevular = new HashSet<randevular>();
        }
        public int USERID { get; set; }
        [Required(ErrorMessage = "Lütfen TC Kimlik No Giriniz", AllowEmptyStrings = false)]
        public string USERTC { get; set; }
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz", AllowEmptyStrings = false)]
        public string USERAD { get; set; }
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz", AllowEmptyStrings = false)]
        public string USERSOYAD { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime USERDOGUM { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 hane olmalıdır")]
        public string USERSİFRE { get; set; }
        [Compare("USERSİFRE", ErrorMessage = "Şifre eşleşmiyor")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string USERRESİFRE { get; set; }
        public int AILEHID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<randevular> randevular { get; set; }
        public virtual doktorlar doktorlar { get; set; }
    }
}