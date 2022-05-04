using System;
using System.Collections.Generic;
using System.Text;

namespace MIUCSHA
{
    class AsistenciasClass
    {
        public string num { get; set; }
        public string nombre { get; set; }
    
        public string nmat { get; set; }
        public string asistio { get; set; }
        public string rut { get; set; }
        public override string ToString()
        {
            return nombre;
        }
    }
}
