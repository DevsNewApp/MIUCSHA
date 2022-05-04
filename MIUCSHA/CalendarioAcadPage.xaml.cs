using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MIUCSHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioAcadPage : ContentPage
    {
        private List<CalendarioClass> Calendario;
        private List<CalenAcadClass> actividades;
        private string Url = "http://localhost:8020/datos";
        private readonly HttpClient client = new HttpClient();
        private PeriodosClass period;
        private string Mes;
        private string mesUpd = "1";
        private string Aurl;
        private string[] meses = new string[] {"Error","Enero","Febrero","Marzo","Abril","Mayo",
            "Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"};
        public CalendarioAcadPage(PeriodosClass periodo, string mes, string Url)
        {
            period = periodo;
            Aurl = Url;
            Mes = mes;
            InitializeComponent();
            DateTime fechaActual = DateTime.Today;
            mesUpd = fechaActual.Month.ToString();
        }
        protected override async void OnAppearing()
        {
            Url = "http://" + Aurl + ":8020/calendario?anop=" + period.anyo;
            int y = Int32.Parse(mesUpd);
            Mesver.Text = meses[y];
            String LasNotas = await client.GetStringAsync(Url);
            actividades = JsonConvert.DeserializeObject<List<CalenAcadClass>>(LasNotas);
            Calendario = new List<CalendarioClass>();
            for (int ty=0; ty< actividades.Count; ty++)
            {
                DateTime parsedDatei = DateTime.Parse(actividades[ty].caa_fini);
                DateTime parsedDatef = DateTime.Parse(actividades[ty].caa_fter);
                string inou = parsedDatei.ToString("dd-MM-yyyy");
                string ouin = parsedDatef.ToString("dd-MM-yyyy");
                if (actividades[ty].mes == mesUpd)
                Calendario.Add(new CalendarioClass { 
                     actividad = actividades[ty].caa_texto,
                      area = actividades[ty].caa_resp,
                       mes = actividades[ty].mes,
                        ini = inou,
                        fin = ouin
                });
            }
            CalendarioAcademico.ItemsSource = Calendario;
            base.OnAppearing();
        }
        void updateData()
        {
            if (actividades.Count > 1)
            {
                Calendario = new List<CalendarioClass>();
                for (int ty = 0; ty < actividades.Count; ty++)
                {
                    DateTime parsedDatei = DateTime.Parse(actividades[ty].caa_fini);
                    DateTime parsedDatef = DateTime.Parse(actividades[ty].caa_fter);
                    string inou = parsedDatei.ToString("dd-MM-yyyy");
                    string ouin = parsedDatef.ToString("dd-MM-yyyy");
                    if (actividades[ty].mes == mesUpd)
                        Calendario.Add(new CalendarioClass
                        {
                            actividad = actividades[ty].caa_texto,
                            area = actividades[ty].caa_resp,
                            mes = actividades[ty].mes,
                            ini = inou,
                            fin = ouin
                        });
                }
                CalendarioAcademico.ItemsSource = Calendario;
            }
        }
        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            y = Int32.Parse(mesUpd);
            y--;
            if (y == 0) y = 12;
            mesUpd = y.ToString();
            Mesver.Text = meses[y];
            updateData();
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            y = Int32.Parse(mesUpd);
            y++;
            if (y == 13) y = 1;
            mesUpd = y.ToString();
            Mesver.Text = meses[y];
            updateData();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}