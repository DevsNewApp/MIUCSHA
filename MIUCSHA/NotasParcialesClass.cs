using System;
namespace MIUCSHA
{
    public class NotasParcialesClass
    {
		public string nro { get; set; }
		public string nmat { get; set; }
		public string nombre { get; set; }
		public string porc_asis { get; set; }
		public string nota { get; set; }
		public string rut { get; set; }
	
		public override string ToString()
		{
			return nombre;
		}
	}
}
