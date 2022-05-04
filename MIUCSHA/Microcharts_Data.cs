using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using Entry = Microcharts.ChartEntry;

namespace MIUCSHA
{
    class Microcharts_Data
    {
        public List<Entry> GetChar()
        {
            List<Entry> data = new List<Entry>
            {
                new Entry(1563532)
                {
                    Label = "01 Ene 16",
                    ValueLabel = "1563532",
                    Color = SKColor.Parse("#FFFF00"),
                    TextColor = SKColor.Parse("#DF013A"),
                },
                new Entry(14088586)
                {
                    Label = "01 Ene 17",
                    ValueLabel = "14088586",
                    Color = SKColor.Parse("#32CD32"),
                    TextColor = SKColor.Parse("#DF013A"),
                },
            };
            return data;
        }
    }
}
