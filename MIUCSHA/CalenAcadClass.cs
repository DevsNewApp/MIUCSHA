using System;
using System.Collections.Generic;
using System.Text;

namespace MIUCSHA
{
    class CalenAcadClass
    {
        public string mes { get; set; }
        public string caa_resp { get; set; }
        public string caa_fini { get; set; }
        public string caa_fter { get; set; }
        public string caa_texto { get; set; }
        
        public override string ToString()
        {
            return caa_texto;
        }
    }
}
