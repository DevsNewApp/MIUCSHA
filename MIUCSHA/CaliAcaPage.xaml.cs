 using System;
using System.Collections.Generic;
using System.Net.Http;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;


namespace MIUCSHA
{
    public partial class CaliAcaPage : ContentPage
    {
        private List<AsistenciaClass> Asiste;
        Microcharts_Data data = new Microcharts_Data();
        private List<cursosClass> dcursos;
        private string rut;
        private string materia;
        private string nombre;
        private string Aurl;
        private string tipo;
        private DateTime diahoy;
        private int rate = 0;
        private string auxilio = "";
        private PeriodosClass periodo;
        private readonly HttpClient client = new HttpClient();
        public CaliAcaPage(List<cursosClass> cursos, string Run, string nmat,string minombre, string Url, PeriodosClass peri)
        {
            rut = Run;
            materia = nmat;
            nombre = minombre;
            dcursos = cursos;
            Aurl = Url;
            periodo = peri;
            diahoy = DateTime.Today;
  
        
            InitializeComponent();
        
            Chartdemo1.Chart = new PieChart{ Entries = data.GetChar() };
        }
        protected   override  void OnAppearing()
        {
            XFecha.Date =  diahoy;
            Anio.Text = periodo.anyo + "-" + periodo.sem;
            //     Nombres.Text = "Docente: " + nombre;
            //     Rut.Text = rut +   "    Carrera: " +  materia;
            var monkeyList = new List<string>();
            for( int r=0; r< dcursos.Count; r++)
                {
                string alfa = ReplaceAt(dcursos[r].codigo, 6, '-');
                monkeyList.Add(alfa);
              
                }
            
            Xpicker.Title = monkeyList[rate];
            Mtitulo.Text = dcursos[rate].asig_desc;
            tipo = dcursos[rate].func_desc;
            Xpicker.ItemsSource = monkeyList;
            string any = periodo.anyo;
            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            string fec = diahoy.ToString("dd-MM-yy");
            string fun = dcursos[rate].hora_func;
            string nrg = dcursos[rate].hora_nrgr;
        //    Msg("Actualizador 1", "No debe ejecutarse en primer lugar");
            Actualizador(any, vep, asi, sec, fec, fun, nrg);
           
        }
            async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async private void Asistencias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AsistenciaClass selec = (AsistenciaClass) Asistencias.SelectedItem;
            string ruts = selec.rut;
            string nmats = selec.nmat;
           
            
            await Navigation.PushModalAsync(new DetalleAlumno(Aurl, ruts,nmats));
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

        private   void Xpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cambio = (string)Xpicker.SelectedItem;
            string titulo = "Evento";
            string cuerpo = "string cambio = " + cambio;
            var monkeyList = new List<string>();
            
            for (int r = 0; r < dcursos.Count; r++)
            {
               
                string alfa = ReplaceAt(dcursos[r].codigo, 6, '-');
                if (alfa.Equals(cambio)) rate = r;
                monkeyList.Add(alfa);

            }

            Xpicker.Title = monkeyList[rate];



            //   await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));

            string okdia = diahoy.ToString("dd-MM-yy");
            string any = periodo.anyo;
            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            string fec = okdia;
            string fun = dcursos[rate].hora_func;
            string nrg = dcursos[rate].hora_nrgr;
        //    Msg("Actualizador 2", "No debe ejecutarse en primer lugar");
            Actualizador(any, vep, asi, sec, fec, fun, nrg);
            //     Xpicker.ItemsSource=
            // Asistencias.ItemsSource = Asiste;
            
        }

        private void XFecha_DateSelected(object sender, DateChangedEventArgs e)
        {
            diahoy = XFecha.Date;
            string okdia = diahoy.ToString("dd-MM-yy");
            string any = periodo.anyo;
            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            string fec = okdia;
            string fun = dcursos[rate].hora_func;
            string nrg = dcursos[rate].hora_nrgr;
       //     Msg("Actualizador 3", "No debe ejecutarse en primer lugar");
            Actualizador(any, vep, asi, sec, fec, fun, nrg);
            //     Xpicker.ItemsSource=
            // Asistencias.ItemsSource = Asiste;
           
        }
        private void Msg(string asunto, string mensaje)
        {
             Device.BeginInvokeOnMainThread(async () =>
              {
            string titulo = asunto;
            string cuerpo = mensaje;
               await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
         //    await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
              });
        }
        protected async void Actualizador(string any0, string vep0, string asi0, string sec0, string fec0, string fun0, string nrg0 )
        {
            string any = any0;
            string vep = vep0;
            string asi = asi0;
            string sec = sec0;
            string fec = fec0;
            string fun = fun0;
            string nrg = nrg0;
            
            //     Xpicker.ItemsSource=
            // Asistencias.ItemsSource = Asiste;
            string URL = "http://" + Aurl + ":8020/aca/asistencias?anyo=" + any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec + "&fecha=" + fec + "&func=" + fun + "&nrgr=" + nrg;
            //  client.Timeout = TimeSpan.FromSeconds(120);
            string content2 = await client.GetStringAsync(URL);

            List<AsistenciasClass> horas = JsonConvert.DeserializeObject<List<AsistenciasClass>>(content2);
            Asiste = new List<AsistenciaClass>();
            int asiste = 0;
            int noasiste = 0;
            string[] ruta = new string[horas.Count];
            int y1 = 0;
         //   if (horas.Count >0) Msg("Primer Loop", "Carga el primer loop");
            for (int yu = 0; yu < horas.Count; yu++)
            {

                int rr = 0;
                for (int ty = 0; ty < y1; ty++)
                {
                    if (horas[yu].rut.Equals(ruta[ty])) rr = 1;
                }
                if (rr == 0)
                {
                    ruta[y1] = horas[yu].rut;
                    y1++;
                    string Url = "http://" + Aurl + ":8020/aca/datosa";
                    Url = Url + "?rut=" + horas[yu].rut + "&nmat=" + horas[yu].nmat;
                    string content = await client.GetStringAsync(Url);
                    List<EstudianteClass> datosA = JsonConvert.DeserializeObject<List<EstudianteClass>>(content);
                    tipo = datosA[0].foto;
                    if (yu == 0)
                        Asiste.Add(new AsistenciaClass
                        {
                            num = horas[yu].num,
                            nombre = horas[yu].nombre,
                            nmat = horas[yu].nmat,
                            rut = horas[yu].rut,
                            asiste = horas[yu].asistio,
                            foto = tipo

                        });
                    else
                        Asiste.Add(new AsistenciaClass
                        {
                            num = horas[yu].num,
                            nombre = horas[yu].nombre,
                            nmat = horas[yu].nmat,
                            rut = horas[yu].rut,
                            asiste = horas[yu].asistio,
                            foto = tipo

                        });
                    if (horas[yu].asistio.Equals("NO")) noasiste++; else asiste++;
                }
            }
            if (horas.Count == 0 && y1==0)
            {
                // Chartdemo2.IsVisible = true;

                try
                {
                    URL = "http://" + Aurl + ":8020/aca/getNotasPar?anyo=" + any + "&vepe=" + vep +
                        "&asig=" + asi + "&secc=" + sec + "&version=" + dcursos[rate].hora_vers +
                        "&func=" + fun + "&nrgr=" + nrg + "&numnota=1" + "&numcontrol=0" + "&tipocredito=" + dcursos[rate].carr_tpro;
                    //  client.Timeout = TimeSpan.FromSeconds(120);
                    content2 = await client.GetStringAsync(URL);
                }
                catch (Exception g)
                {
                    string titulo = "Conexion";
                    string cuerpo = "El servidor no responde posible problema de conexion";
                    // cuerpo = g.ToString();
                    await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                }
                List<NotasParcialesClass> noticas = JsonConvert.DeserializeObject<List<NotasParcialesClass>>(content2);

                Asiste = new List<AsistenciaClass>();
                string[] rutas = new string[noticas.Count];
                int yy = 0;
                
              //  if (noticas.Count > 0)  Msg("Segundo Loop", "Carga el segundo loop");
                for (int yuk = 0; yuk < noticas.Count; yuk++)
                {
                    int rr = 0;
                    string valores = noticas[yuk].rut;
                    for (int ty = 0; ty < yy; ty++)
                    {
                     //   Msg(valores, rutas[ty]);
                        if (valores.Equals(rutas[ty]))
                        {

                      //      Msg("Igual", "Encontro uno identico");
                            rr = 1;
                        }
                    }
                    if (rr == 0 && !auxilio.Equals(noticas[yuk].rut))
                    {
                        auxilio = noticas[yuk].rut;
                        rutas[yy] = noticas[yuk].rut;
                      //  Msg("RUT:", noticas[yuk].rut);
                        yy++;

                        string Url = "http://" + Aurl + ":8020/aca/datosa";
                        Url = Url + "?rut=" + noticas[yuk].rut + "&nmat=" + noticas[yuk].nmat;
                        client.GetStringAsync(Url).Wait();
                        string content = client.GetStringAsync(Url).Result;
                            List<EstudianteClass> datosA = JsonConvert.DeserializeObject<List<EstudianteClass>>(content);
                            tipo = datosA[0].foto;
                            Asiste.Add(new AsistenciaClass
                            {
                                num = noticas[yuk].nro,
                                nombre = noticas[yuk].nombre,
                                nmat = noticas[yuk].nmat,
                                rut = noticas[yuk].rut,
                                asiste = " ",
                                foto = tipo


                            });
                       
                    }
                   
                }

            }
            else
            {
                Chartdemo1.IsVisible = true;
            }
            Asistencias.ItemsSource = Asiste;
            if (horas.Count != 0)
            {
                List<Entry> datas = new List<Entry>();

                datas.Add(new Entry(noasiste)
                {
                    Label = "01 Ene 16",
                    ValueLabel = "1563532",
                    Color = SKColor.Parse("#FFFF00"),
                    TextColor = SKColor.Parse("#DF013A"),
                });
                datas.Add(new Entry(asiste)
                {
                    Label = "01 Ene 17",
                    ValueLabel = "14088586",
                    Color = SKColor.Parse("#32CD32"),
                    TextColor = SKColor.Parse("#DF013A"),
                });

                Chartdemo1.Chart = new PieChart { Entries = datas };
            }




        }




    }
}
