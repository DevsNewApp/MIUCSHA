using System;
namespace MIUCSHA
{
    public class ProgramasClass
    {
        public string codigo { get; set; }
        public string programa { get; set; }
        public string jornada { get; set; }
        public override string ToString()
        {
            return programa;
        }
    }
}
