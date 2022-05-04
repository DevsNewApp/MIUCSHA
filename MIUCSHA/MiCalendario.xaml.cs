using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class MiCalendario : ContentPage
    {
        private ObservableCollection<Horarios> _hora;
        public IList<Horarios> Hora { get; private set; }
        private List<HorariosClases> horitas;
        public List<cursosClass> carga;
        private string docentes;
        private string sec;
        private string sala;
        private string[] diasSem = new string[6] { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES", "SABADO" };
        private string hoy = "";
        private string Aurl = "127.0.0.1";
        private PeriodosClass perio;
        private int i = 0;
        private string AsignaNombre(string nombre)
        {
            string alfa = nombre;
            
            int aux = -1;
            string devuelve = nombre.Substring(0, nombre.IndexOf('-'));
            sec = "";
            docentes = "";
            sala = "";
            if ( nombre.IndexOf('-') > 2 && nombre.IndexOf('/')> 2)
            {

            
                sala = nombre.Substring(nombre.IndexOf('/') + 1);
            
                for (int y = 0; y < carga.Count; y++)
                {
                    string compara = carga[y].codigo;
                    alfa = nombre.Substring(0, nombre.IndexOf('-'));
                    if (alfa.Equals(compara)) aux = y;

                }
                if (aux != -1)
                {
                    devuelve = carga[aux].asig_desc;
                    docentes = "  " ;
                    sec = "Seccion:"+ carga[aux].hora_secc + "   Sala:"+sala;
                }
            }
            return devuelve;
        }
        public  MiCalendario(string Durl, List<HorariosClases> horas, List<cursosClass> cargas, string dia, PeriodosClass per)
        {
            string linea = "0";
            string apartado = "nulo";
            Aurl = Durl;
            perio = per;
            InitializeComponent();
           
            hoy = dia;
            carga = cargas;
            horitas = horas;

            string[]  hor = new string[9] { "08:30-09:50", "10:00-11:20", "11:30-12:50", "13:00-14:20", "15:20-16:40", "16:50-18:10", "18:20-19:40", "19:50-21:10", "21:20-22:40" };
            string visi = "true";
            DiaSem.Text = dia;
            i = 0;
            linea = "10";
            try
            {
                Hora = new List<Horarios>();
                linea = "20";
                if (dia.Equals("LUNES"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {


                        if (horas[tr].lunes != "")
                        {
                            i++;
                            string nombre = AsignaNombre(horas[tr].lunes);
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }

                            }
                            Hora.Add(new Horarios
                            {

                                Dia = "LUNES",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            visi = "false";
                        }
                    }
                visi = "true";
                if (dia.Equals("MARTES"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {

                        if (horas[tr].martes != "")
                        {
                            i++;
                            string nombre = AsignaNombre(horas[tr].martes);
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }
                            }
                            Hora.Add(new Horarios
                            {

                                Dia = "MARTES",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            visi = "false";
                        }
                    }
                visi = "true";
                linea = "30";
                if (dia.Equals("MIERCOLES"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {
                        linea = "32";
                        if (horas[tr].miercoles != "")
                        {
                            i++;
                            apartado = horas[tr].lunes;
                            string nombre = AsignaNombre(horas[tr].miercoles);
                            linea = "33";
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                linea = "34";
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    linea = "35";
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }
                            }
                            linea = "36";
                            Hora.Add(new Horarios
                            {

                                Dia = "MIERCOLES",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            linea = "37";
                            visi = "false";
                        }
                    }
                visi = "true";
                linea = "40";
                if (dia.Equals("JUEVES"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {

                        if (horas[tr].jueves != "")
                        {
                            i++;
                            string nombre = AsignaNombre(horas[tr].jueves);
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }
                            }
                            Hora.Add(new Horarios
                            {

                                Dia = "JUEVES",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            visi = "false";
                        }

                    }
                visi = "true";
                if (dia.Equals("VIERNES"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {

                        if (horas[tr].viernes != "")
                        {
                            i++;
                            string nombre = AsignaNombre(horas[tr].viernes);
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }
                            }
                            Hora.Add(new Horarios
                            {

                                Dia = "VIERNES",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            visi = "false";
                        }
                    }
                visi = "true";
                if (dia.Equals("SABADO"))
                    for (var tr = 0; tr < horas.Count; tr++)
                    {


                        if (horas[tr].sabado != "")
                        {
                            i++;
                            string nombre = AsignaNombre(horas[tr].sabado);
                            for (int t7 = 0; t7 < cargas.Count; t7++)
                            {
                                if (cargas[t7].hora_asig.Equals(nombre))
                                {
                                    docentes = cargas[t7].asig_desc;
                                    sec = cargas[t7].hora_secc;
                                }
                            }
                            Hora.Add(new Horarios
                            {

                                Dia = "SABADO",
                                Materia = nombre,
                                Docente = "  " + docentes,
                                Seccion = sec,
                                Horario = hor[tr],
                                Visible = visi,
                            });
                            visi = "false";
                        }
                    }
            } catch(Exception f)
            {
                string titulo = "Atencion";
                string cuerpo = "Tenemos problemas cargando el calendario en la linea "+ linea + "aprt: "+ apartado;

                PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
              
            }
           
            BindingContext = this;

        }
        protected override async void OnAppearing()
        {
            string an = perio.anyo;
            string se = perio.sem;
            string tit = "HORARIOS ("+an + " - " + se+")";
            titulos.Text = tit;
            if (i == 0)
            {
                string titulo = "Atencion";
                string cuerpo = "En este dia usted no tiene actividades";

                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                await Navigation.PopModalAsync();
            }
            base.OnAppearing();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        void recall(string dia)
        {
            string[] hor = new string[9] { "08:30-09:50", "10:00-11:20", "11:30-12:50", "13:00-14:20", "15:20-16:40", "16:50-18:10", "18:20-19:40", "19:50-21:10", "21:20-22:40" };
            string visi = "true";
            string asi = "";
            DiaSem.Text = dia;
            Hora = new List<Horarios>();
            for (var tr = 0; tr < horitas.Count; tr++)
                {

                if (dia.Equals("LUNES")) asi = horitas[tr].lunes;
                if (dia.Equals("MARTES")) asi = horitas[tr].martes;
                if (dia.Equals("MIERCOLES")) asi = horitas[tr].miercoles;
                if (dia.Equals("JUEVES")) asi = horitas[tr].jueves;
                if (dia.Equals("VIERNES")) asi = horitas[tr].viernes;
                if (dia.Equals("SABADO")) asi = horitas[tr].sabado;
                string nombre = "";
                if (asi.Length > 1)  nombre = AsignaNombre(asi);
                for (int t7 = 0; t7 < carga.Count; t7++)
                {
                    if (carga[t7].hora_asig.Equals(nombre))
                    {
                        docentes = carga[t7].asig_desc;
                        sec = carga[t7].hora_secc;
                    }
                }
                if (asi != "")
                  {
                        Hora.Add(new Horarios
                        {

                            Dia = dia,
                            Materia = nombre,
                            Docente = "  " + docentes,
                            Seccion = sec,
                            Horario = hor[tr],
                            Visible = visi,
                        });
                        visi = "false";
                  }
             }
            Horario.ItemsSource = Hora ;
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            for (int r = 0; r < 6; r++) if (diasSem[r].Equals(hoy)) y = r;
            y = (y-1 < 0) ? 5 : y-1;
            hoy = diasSem[y];
            recall(hoy);
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            for (int r = 0; r < 6; r++) if (diasSem[r].Equals(hoy)) y = r;
            y = (y + 1 > 5) ? 0 : y + 1;
            hoy = diasSem[y];
            recall(hoy);
        }
    }
}
