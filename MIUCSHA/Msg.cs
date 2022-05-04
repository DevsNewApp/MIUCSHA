﻿using System;

namespace MIUCSHA
{
    public class Msg
    {
        public string _id { get; set; }
        public string destinatario { get; set; }
        public string remitente { get; set; }
        public string recepcion { get; set; }
        public string asunto { get; set; }
        public string cuerpo { get; set; }
        public DateTime create_date { get; set; }
        public override string ToString()
        {
            return asunto;
        }
        
    }
}
