using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class CalificacionPage : ContentPage
    {
        private ObservableCollection<Notas> _nota;
        public IList<Notas> Nota { get; private set; }
        private string Url = "http://localhost:8020/datos";
        private readonly HttpClient client = new HttpClient();
        private List<NotasClass> notas;
        public List<string> Asignaturas;
        private string nmat = "0";
        private string anyo = "2020";
        private string sem = "1";
        private string busqueda = "";
        private List<CargaClass> carga;
        private string Aurl = "127.0.0.1";
        private PeriodosClass perio;

        public CalificacionPage(string Durl,string bus, List<CargaClass>cargaAca, PeriodosClass period, string numat)
        {

            // picker.Text = bus;
            perio = period;
           
            
            Aurl = Durl;
            busqueda = bus;
            carga = new List<CargaClass>();
            notas = new List<NotasClass>();
            Nota = new List<Notas>();
            carga = cargaAca;
            anyo = period.anyo;
            sem = period.sem;
            nmat = numat;
            Nota.Add(new Notas {
             Asignatura="No registra Notas",
              Fecha = "01-01-2020",
              Nota = "0.0",

              Ponderacion = "0",
             Tipo="NoEval",
              Visible="true"
            });
            InitializeComponent();
            
        }
       
            async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
  
        protected override async void OnAppearing()
        {
            string asig = "";
            string secc = "";
            string version = "";
            string codFuncion = "";
            string grupo = "";
            string vali = perio.anyo + "-" + perio.sem;
            titulo.Text = "NOTAS (" +vali+")";
            Mater.Text = busqueda;
            for (var gt = 0; gt < carga.Count; gt++)
            {
                if (String.Equals(busqueda, carga[gt].desc_asig) )
                {
                    asig = carga[gt].asig;
                    secc = carga[gt].seccion;
                    version = carga[gt].version;
                    codFuncion = carga[gt].cod_func;
                    grupo = carga[gt].grupo;

                }
            }
            // Url = "http://"+Aurl+":8020/notas?codigo="+nmat+"1611046&anyo=2019&sem=2&asig=ESDE03&secc=3&version=0&codFuncion=01&grupo=0";
            Url = "http://"+Aurl+":8020/notas?codigo=" + nmat + "&anyo=" + anyo + "&sem=" + sem + "&asig=" + asig + "&secc=" + secc + "&version=" + version + "&codFuncion=" + codFuncion + "&grupo=" + grupo;

            String LasNotas = await client.GetStringAsync(Url);
            notas = JsonConvert.DeserializeObject<List<NotasClass>>(LasNotas);
            Nota = new List<Notas>();
            int canti = 0;
            for (int yu = 0; yu < notas.Count; yu++)
            {
                 String valorDia = notas[yu].fecha;
                if (valorDia.Length > 10)
                {
                    valorDia = valorDia.Substring(0, 10);
                    DateTime oDate = DateTime.ParseExact(valorDia, "yyyy-MM-dd", null);
                    valorDia = oDate.Day+"-"+oDate.Month + "-" + oDate.Year;
                }
                else valorDia = " ";
                // DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt", null);
                // if (notas[yu].fecha.Length >2 ) valorDia = Convert.ToDateTime(notas[yu].fecha);
                if (valorDia.Length > 5)
                {
                    canti++;
                    if (notas[yu].nota.Equals("undefined")) notas[yu].nota = "0.0";
                    Nota.Add(new Notas
                    {
                        Asignatura = notas[yu].asig,
                        Fecha = valorDia,
                        Tipo = notas[yu].tino,
                        Ponderacion = notas[yu].pond,
                        Nota = notas[yu].nota,
                        Visible = "true",

                    });
                }
                else
                {
                    Final.IsVisible = true;
                    //  await DisplayAlert("Notificacion", "Este es el valor = " + notas[yu].nota, "OK");
                    if (!notas[yu].nota.Equals("undefined"))
                        Final.Text = notas[yu].nota;
                    else
                    {
                        Final.Text = "-";
                        Final.IsVisible = false;
                        etiqueta.IsVisible = false;
                    }
                }
            }
            if (canti==0)
            {
                string titulo = "Atencion";
                string cuerpo = "Usted aun no tiene calificaciones para esta materia";

                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                await Navigation.PopModalAsync();
            }
            // await DisplayAlert("Notificacion", "Todos estan = " + picker.SelectedItem, "OK");
            Notas.ItemsSource = Nota;
            base.OnAppearing();
        }


    }
}
