using System;
using System.Collections.Generic;
using System.Text;

namespace MIUCSHA
{
    class UsuarioClass
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string tipo { get; set; }
   
        public DateTime create_date { get; set; }
        public override string ToString()
        {
            return nombre;
        }
    }
}
