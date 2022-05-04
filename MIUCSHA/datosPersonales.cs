using System;
namespace MIUCSHA
{
    public class datosPersonales
    {
        public string numMat { get; set; }
        public string apellPat { get; set; }
        public string apellMat { get; set; }
        public string nombres { get; set; }
        public string codCar { get; set; }
        public string carrera { get; set; }
        public override string ToString()
        {
            return numMat;
        }
    }
}
