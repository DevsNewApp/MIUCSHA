﻿using System;
namespace MIUCSHA
{
    public class EscuelaClass
    {
        public string codigo { get; set; }
        public string escuela { get; set; }

        public override string ToString()
        {
            return escuela;
        }
    }
}
