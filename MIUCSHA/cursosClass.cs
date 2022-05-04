using System;
namespace MIUCSHA
{
    public class cursosClass
    {
        public string hora_anoa { get; set; }
        public string hora_vepe { get; set; }
        public string hora_asig { get; set; }
        public string hora_secc { get; set; }
        public string asig_nomc { get; set; }
        public string asig_desc { get; set; }
        public string sede      { get; set; }
        public string hora_vers { get; set; }
        public string hora_clases { get; set; }
        public string hora_func { get; set; }
        public string func_desc { get; set; }
        public string hora_nrgr { get; set; }
        public string codigo { get; set; }
        public string carr_tpro { get; set; }
        public string estudiantes { get; set; }
        public override string ToString()
        {
            return asig_desc;
        }
    }
}
