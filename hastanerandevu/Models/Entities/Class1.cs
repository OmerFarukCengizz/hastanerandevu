using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hastanerandevu.Models.Entities
{
    public class Class1
    {
        public int SEHIRID { get; set; }
        public int ILCEID { get; set; }
        public int HASTANEID { get; set; }
        public int KLINIKID { get; set; }
        public int DOKTORID { get; set; }
        public string HASTANEAD { get; set; }
        public virtual ilceler ilceler { get; set; }
        public virtual sehirler sehirler { get; set; }

    }
}