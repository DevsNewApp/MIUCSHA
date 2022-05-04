using System;
namespace MIUCSHA
{
    public class ProAsisteClass
    {
   
        public string num { get; set; }
        public string tias_desc { get; set; }
        public string aspr_fech { get; set; }
        public string aspr_modu { get; set; }
        public string aspr_minu { get; set; }
        public override string ToString()
        {
            return tias_desc;
        }
    }
}
