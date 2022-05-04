using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

using SkiaSharp;
using Rg.Plugins.Popup.Services;


namespace MIUCSHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsisteDocePage : ContentPage
    {
        private List<AsistenciaClass> Asiste;
        private List<cursosClass> dcursos;
        private int rate = 0;
        private PeriodosClass periodo;
        private string Aurl;
        Microcharts_Data data = new Microcharts_Data();
        private int term = 0;
        private DateTime diahoy;
        private string asignatura = "";
        private List<AsisteAcaClass> Asis;
        List<string> monkeyList;
        private readonly HttpClient client = new HttpClient();
        public AsisteDocePage(List<cursosClass> cursos, string Durl, string Run, string minombre, PeriodosClass peri)
        {
            Aurl = Durl;
            dcursos = cursos;
            periodo = peri;
            Asiste = new List<AsistenciaClass>();
            Asiste.Add(new AsistenciaClass
            {
                num = "3/03/2020",
                rut = "0",
                nombre = "401",
                asiste = "NO",
                foto = "Asistencia"

            });
            Asiste.Add(new AsistenciaClass
            {
                num = "20/03/2020",
                rut = "0",
                nombre = "509",
                asiste = "NO",
                foto = "Asistencia"

            });
            Asiste.Add(new AsistenciaClass
            {
                num = "05/04/2020",
                rut = "43",
                nombre = "611",
                asiste = "NO",
                foto = "Atraso"

            });
            InitializeComponent();
            Chartdemo1.Chart = new PieChart { Entries = data.GetChar() };
        }

        async protected override void OnAppearing()
        {
            try
            {
                string any = periodo.anyo; // "2019";
                string vep = periodo.sem; // "2";
                string asi = "EDF001";
                string sec = "1";
                string funci = "01";
                // asiste? anyo = 2019 & vepe = 2 & asig = ESF062 & secc = 2 & func = 03 & fecha = 2019 - 08 - 26


                monkeyList = new List<string>();
                for (int r = 0; r < dcursos.Count; r++)
                {
                    string alfa = ReplaceAt(dcursos[r].codigo, 6, '-');
                    monkeyList.Add(alfa);

                }
                diahoy = DateTime.Now;
                string okdia = diahoy.ToString("yyyy-MM-dd");
                XFecha.Date = diahoy;
                Anio.Text = periodo.anyo + "-" + periodo.sem;
                Xpicker.Title = monkeyList[rate];
                Xpicker.ItemsSource = monkeyList;
                Titcurso.Text = dcursos[0].hora_asig + "-" + dcursos[0].hora_secc + " " + dcursos[0].asig_desc;
                Asistencias.ItemsSource = Asiste;
                asi = dcursos[term].hora_asig;
                sec = dcursos[term].hora_secc;
                funci = dcursos[term].hora_func;
                string Fechasa = okdia;

                string URL = "http://" + Aurl + ":8020/aca/asiste?anyo=" +
                    any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec + "&func=" + funci + "&fecha=" + Fechasa;
                // client.Timeout = TimeSpan.FromSeconds(120);
                string content2 = await client.GetStringAsync(URL);

                Asis = JsonConvert.DeserializeObject<List<AsisteAcaClass>>(content2);
                int asistente = 0;
                int noasistente = 0;
                asistente = 0;
                Asiste = new List<AsistenciaClass>();
                if (Asis.Count < 1)
                {
                    Asiste.Add(new AsistenciaClass
                    {
                        num = " ",
                        nombre = "Datos",
                        rut = " ",
                        asiste = " ",
                        foto = "Fecha sin"
                    });
                }
                for (int hy = 0; hy < Asis.Count; hy++)
                {
                    asistente++;
                    Asiste.Add(new AsistenciaClass
                    {
                        num = Asis[hy].aspr_fech.Substring(0, 10),
                        rut = Asis[hy].aspr_minu,
                        nombre = Asis[hy].aspr_modu,
                        asiste = "NO",
                        foto = Asis[hy].tias_desc

                    });
                }
                Asistencias.ItemsSource = Asiste;
                List<Entry> datas = new List<Entry>();
                datas.Add(new Entry(noasistente)
                {
                    Label = "01 Ene 16",
                    ValueLabel = "0",
                    Color = SKColor.Parse("#FFFF00"),
                    TextColor = SKColor.Parse("#DF013A"),
                });
                datas.Add(new Entry(asistente)
                {
                    Label = "01 Ene 17",
                    ValueLabel = "2",
                    Color = SKColor.Parse("#32CD32"),
                    TextColor = SKColor.Parse("#DF013A"),
                });

                Chartdemo1.Chart = new PieChart { Entries = datas };
            } catch(Exception p)
            {
                Msg("Error 01001", p.ToString());
            }
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private static string ReplaceAt(string value, int index, char newchar)
        {
            if (value == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = value.ToCharArray();
            chars[index] = newchar;
            return new string(chars);
        }
         private void Asistencias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string selec = Asistencias.SelectedItem.ToString();

            // Page p = new DetalleAlumno("ttut","66262","686867");
           // await Navigation.PushModalAsync(p);
        }
        private void Msg(string asunto, string mensaje)
        {
           // Device.BeginInvokeOnMainThread(async () =>
          //  {
                string titulo = asunto;
                string cuerpo = mensaje;
          //     await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                // await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
          //  });
        }
        async void XFecha_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            try
            {
                string any = periodo.anyo;
                string vep = periodo.sem;
                string asi = asignatura;
                string sec = "";
                string funci = "";
                asi = dcursos[term].hora_asig;
                sec = dcursos[term].hora_secc;
                funci = dcursos[term].hora_func;
                diahoy = XFecha.Date;
                string okdia = diahoy.ToString("yyyy-MM-dd");
                string URL = "http://" + Aurl + ":8020/aca/asiste?anyo=" +
                   any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec + "&func=" + funci + "&fecha=" + okdia;
                // client.Timeout = TimeSpan.FromSeconds(120);
                Msg("URL", URL);
                string content2 = await client.GetStringAsync(URL);

                Asis = JsonConvert.DeserializeObject<List<AsisteAcaClass>>(content2);
                int asistente = 0;
                int noasistente = 0;
                asistente = 0;
                Asiste = new List<AsistenciaClass>();
                if (Asis.Count < 1)
                {
                    Asiste.Add(new AsistenciaClass
                    {
                        num = " ",
                        nombre = "Datos",
                        rut = " ",
                        asiste = " ",
                        foto = "Fecha sin"
                    });
                }
                for (int hy = 0; hy < Asis.Count; hy++)
                {
                    asistente++;
                    Asiste.Add(new AsistenciaClass
                    {
                        num = Asis[hy].aspr_fech.Substring(0, 10),
                        rut = Asis[hy].aspr_minu,
                        nombre = Asis[hy].aspr_modu,
                        asiste = "NO",
                        foto = Asis[hy].tias_desc

                    });
                }
                Asistencias.ItemsSource = Asiste;
                List<Entry> datas = new List<Entry>();
                datas.Add(new Entry(noasistente)
                {
                    Label = "01 Ene 16",
                    ValueLabel = "0",
                    Color = SKColor.Parse("#FFFF00"),
                    TextColor = SKColor.Parse("#DF013A"),
                });
                datas.Add(new Entry(asistente)
                {
                    Label = "01 Ene 17",
                    ValueLabel = "2",
                    Color = SKColor.Parse("#32CD32"),
                    TextColor = SKColor.Parse("#DF013A"),
                });

                Chartdemo1.Chart = new PieChart { Entries = datas };
            } catch(Exception m)
            {
                Msg("Error cambio Fscha", m.ToString());
            }
        }

        async void Xpicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            int j = Xpicker.SelectedIndex;
            term = j;
            string any = periodo.anyo;
            string vep = periodo.sem;
            string asi = asignatura;
            string sec = "";
            string funci = "";
            asi = dcursos[term].hora_asig;
            sec = dcursos[term].hora_secc;
            funci = dcursos[term].hora_func;
            string okdia = diahoy.ToString("yyyy-MM-dd");
            string URL = "http://" + Aurl + ":8020/aca/asiste?anyo=" +
               any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec + "&func=" + funci + "&fecha=" + okdia;
            // client.Timeout = TimeSpan.FromSeconds(120);
            Msg("URL", URL);
            string content2 = await client.GetStringAsync(URL);

            Asis = JsonConvert.DeserializeObject<List<AsisteAcaClass>>(content2);
            int asistente = 0;
            int noasistente = 0;
            asistente = 0;
            Asiste = new List<AsistenciaClass>();
            if (Asis.Count < 1)
            {
                Asiste.Add(new AsistenciaClass
                {
                    num = " ",
                    nombre = "Datos",
                    rut = " ",
                    asiste = " ",
                    foto = "Fecha sin"
                });
            }
            for (int hy = 0; hy < Asis.Count; hy++)
            {
                asistente++;
                Asiste.Add(new AsistenciaClass
                {
                    num = Asis[hy].aspr_fech.Substring(0, 10),
                    rut = Asis[hy].aspr_minu,
                    nombre = Asis[hy].aspr_modu,
                    asiste = "NO",
                    foto = Asis[hy].tias_desc

                });
            }
            Asistencias.ItemsSource = Asiste;
            List<Entry> datas = new List<Entry>();
            datas.Add(new Entry(noasistente)
            {
                Label = "01 Ene 16",
                ValueLabel = "0",
                Color = SKColor.Parse("#FFFF00"),
                TextColor = SKColor.Parse("#DF013A"),
            });
            datas.Add(new Entry(asistente)
            {
                Label = "01 Ene 17",
                ValueLabel = "2",
                Color = SKColor.Parse("#32CD32"),
                TextColor = SKColor.Parse("#DF013A"),
            });

            Chartdemo1.Chart = new PieChart { Entries = datas };
        }

        void XFecha_DateSelected_1(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            Msg("Msg", "Este es el mensaje");
        }
    }
}