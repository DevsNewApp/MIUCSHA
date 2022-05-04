using System;
using System.Collections.Generic;
using System.Text;

namespace MIUCSHA
{
    class NotaFinalClass
    {
		public string num { get; set; }
		public string nombre { get; set; }
		public string nmat { get; set; }
		public string notaf { get; set; }
		
		public string codsitf { get; set; }
		public string descsitf { get; set; }

		public string alum_rut { get; set; }

		public override string ToString()
		{
			return nombre;
		}
	}
}
