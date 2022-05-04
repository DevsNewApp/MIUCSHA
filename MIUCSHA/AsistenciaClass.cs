using System;
namespace MIUCSHA
{
    public class AsistenciaClass
    {
		public string num { get; set; }
		public string nmat { get; set; }
		public string rut { get; set; }
		public string nombre { get; set; }
		public string asiste { get; set; }
		public string foto { get; set; }
		public override string ToString()
		{
			return nombre;
		}
	}
}
